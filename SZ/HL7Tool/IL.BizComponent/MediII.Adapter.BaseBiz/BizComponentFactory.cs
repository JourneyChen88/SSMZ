using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediII.Adapter.BaseBiz
{
    public static class BizComponentFactory
    {
        static readonly string HosiptalName;
        static List<string> listNotExist = new List<string>();

        static BizComponentFactory()
        {
            //HosiptalName = System.Configuration.ConfigurationManager.AppSettings["HosiptalName"];
        }

        public static IBizComponent GetBizComponent(string messageName)
        { 
            string assemblyName = string.Format("MediII.Adapter.BizComponent.{0}",messageName);
            string typeName ;
            if (string.IsNullOrEmpty(HosiptalName) || listNotExist.Contains(messageName))
            {
                typeName = string.Format("{0}.BizComponent_{1}", assemblyName, messageName);
                try
                {
                    return Activator.CreateInstance(assemblyName, typeName).Unwrap() as IBizComponent;
                }
                catch
                {
                    throw new Exception(string.Format("创建消息处理对象{0}失败", typeName));
                }
            }
            else
            {
                typeName = string.Format("{0}.BizComponent_{1}_{2}", assemblyName, messageName, HosiptalName);

                try
                {
                    return Activator.CreateInstance(assemblyName, typeName).Unwrap() as IBizComponent;
                }
                catch (TypeLoadException)
                {
                    typeName = string.Format("{0}.BizComponent_{1}", assemblyName, messageName);
                    var bizComponet = Activator.CreateInstance(assemblyName, typeName).Unwrap() as IBizComponent;
                    lock (listNotExist)
                    {
                        listNotExist.Add(messageName);
                    }
                    return bizComponet;
                }
                catch
                {
                    throw new Exception(string.Format("创建消息处理对象{0}失败", typeName));
                }
            }
        }
    }
}
