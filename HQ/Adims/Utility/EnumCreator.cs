using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace adims_Utility
{
    /// <summary>
    /// 枚举构造器
    /// </summary>
    public class EnumCreator
    {
        public enum ReadModel
        {
            /// <summary>
            /// 
            /// </summary>
            可修改 = 0,
            只读 ,
        }

        public enum UserType
        {
            /// <summary>
            /// 
            /// </summary>
            管理员 = 0,
            麻醉医生 = 1,
            护士 = 2,
        }
        /// <summary>
        /// 排班状态
        /// </summary>
        public enum Ostate
        {
            /// <summary>
            /// 
            /// </summary>
            未排班 = 0,
            已排班 = 1,
            手术开始 = 2,
            手术结束 = 3,

        }

        /// <summary>
        /// 流程类型
        /// </summary>
        public enum FlowType
        {
            /// <summary>
            /// 未设定
            /// </summary>
            NotSet = 0,

            /// <summary>
            /// 排班
            /// </summary>
            Paiban,
            /// <summary>
            /// 手术植入物记录
            /// </summary>
            OperImplant,

            /// <summary>
            /// 麻醉知情同意书
            /// </summary>
            Mzzqtys,

            /// <summary>
            /// 术前访视医生
            /// </summary>
            BeforeVisit_YS,

            /// <summary>
            /// 术前访视-护士
            /// </summary>
            BeforeVisit_HS,

            /// <summary>
            /// 麻醉记录单
            /// </summary>
            Mzjld,

            /// <summary>
            /// 器械清点
            /// </summary>
            QixieQingdian,

            /// <summary>
            /// 麻醉恢复
            /// </summary>
            PACU,

            /// <summary>
            /// 护理记录
            /// </summary>
            NurseRecord,

            /// <summary>
            /// 麻醉镇痛
            /// </summary>
            AfterAnalgesia,
            /// <summary>
            /// 临时医嘱
            /// </summary>
            Lsyz,
            /// <summary>
            /// 术后随访
            /// </summary>
            AfterVisit,
            /// <summary>
            /// 麻醉总结
            /// </summary>
            AnesthesiaSummary,
            /// <summary>
            /// 输血评估
            /// </summary>
            TransfusionEvaluation,

            /// <summary>
            /// 镇痛治疗同意书
            /// </summary>
            ZTZLTYS,





        }

        /// <summary>
        /// 用药类型
        /// </summary>
        public enum YongyaoType
        {
            /// <summary>
            /// 气体药
            /// </summary>
            气体药 = 1,

            /// <summary>
            /// 诱导药
            /// </summary>
            诱导药,

            /// <summary>
            /// 局麻药
            /// </summary>
            局麻药,

            /// <summary>
            /// 输液
            /// </summary>
            输液,

            /// <summary>
            /// 输血
            /// </summary>
            输血,

            /// <summary>
            /// 其他用药
            /// </summary>
            其他用药,


           
        }

        /// <summary>
        /// 前缀码
        /// </summary>
        public enum PrefixCode
        {
            /// <summary>
            /// 系统命令编码
            /// </summary>
            SystemCommandCode = 00,
            /// <summary>
            /// 用户编码
            /// </summary>
            UserCode = 01,//用户编码
            /// <summary>
            /// 生产商编码
            /// </summary>
            ManufacturerCode = 02,
            /// <summary>
            /// 供应商编码
            /// </summary>
            SupplierCode = 03,
            /// <summary>
            /// 仓库库位编码
            /// </summary>
            WarehouseCode = 04,
            /// <summary>
            /// 器械类型编码
            /// </summary>
            InstrumentTypeCode = 05,
            /// <summary>
            /// 一次性物品类型编码
            /// </summary>
            GoodsTypeCode = 06,
            /// <summary>
            /// 辅料类型编码
            /// </summary>
            AuxiliaryMaterialTypeCode = 07,
            /// <summary>
            /// 包类型编码
            /// </summary>
            PackageTypesCode = 08,
            /// <summary>
            /// 消耗品类型编码
            /// </summary>
            ConsumablesTypeCode = 09,
            /// <summary>
            /// 外包装编码
            /// </summary>
            OuterPackageCode = 10,
            /// <summary>
            /// 设备编码
            /// </summary>
            EquipmentCode = 11,
            /// <summary>
            /// 容器编码
            /// </summary>
            ContainerCode = 12,
            /// <summary>
            /// 科室编码
            /// </summary>
            DepartmentCode = 16,
            /// <summary>
            /// 入库编码
            /// </summary>
            StorageInCode = 20,
            /// <summary>
            /// 出库编码
            /// </summary>
            StorageOutCode = 22,
            /// <summary>
            /// 回收编码
            /// </summary>
            RecycleCode = 23,
            /// <summary>
            /// 清洗编码
            /// </summary>
            DisinfectCode = 24,
            /// <summary>
            /// 发放编码
            /// </summary>
            ProvideCode = 30,
            /// <summary>
            /// 预定单号
            /// </summary>
            ReserveCode = 32,
            /// <summary>
            /// 配包条码
            /// </summary>
            AllocatingPackageCode = 33,
            /// <summary>
            /// 灭菌准备条码
            /// </summary>
            SterilizationPrepareCode = 34,
            /// <summary>
            /// 灭菌条码
            /// </summary>
            SterilizationCode = 35,
            /// <summary>
            /// 产品包编码
            /// </summary>
            PackageOfProductCode = 40,
            /// <summary>
            /// 产品一次性物品编码
            /// </summary>
            GoodsOfProductCode = 41,
            /// <summary>
            /// 产品辅料编码
            /// </summary>
            AuxiliaryMaterialOfProductCode = 42,
            /// <summary>
            /// 产品消耗品代码
            /// </summary>
            ConsumablesOfProductCode = 43,
            /// <summary>
            /// 产品器械代码
            /// </summary>
            InstrumentOfProductCode = 48,
            /// <summary>
            /// 包编码
            /// </summary>
            PackageCode = 60,
            /// <summary>
            /// 一次性物品编码
            /// </summary>
            GoodsCode = 61,
            /// <summary>
            /// 辅料编码
            /// </summary>
            AuxiliaryMaterialCode = 62,
            /// <summary>
            /// 消耗品代码
            /// </summary>
            ConsumablesCode = 63,
            /// <summary>
            /// 设备使用代码
            /// </summary>
            DeviceUseCode = 66,
            /// <summary>
            /// 器械代码
            /// </summary>
            InstrumentCode = 68,
            /// <summary>
            /// 消毒灭菌程序
            /// </summary>
            DisinfectionCode = 73
        }

        public enum ScanResult
        {

            /// <summary>
            /// 成功
            /// </summary>
            Success = 1,

            /// <summary>
            /// 失败
            /// </summary>
            Fail = 2,
            /// <summary>
            /// 警告
            /// </summary>
            [Description("警告")]
            Warming = 3,
            /// <summary>
            /// 提示
            /// </summary>
            Tooltip = 4,
        }

        /// <summary>
        /// 基本操作
        /// </summary>
        public enum MenuOperate
        {
            /// <summary>
            /// 查询
            /// </summary>
            [Description("查询")]
            Search = 1,

            /// <summary>
            /// 新建
            /// </summary>
            [Description("新建")]
            New = 2,
            /// <summary>
            /// 修改
            /// </summary>
            [Description("修改")]
            Edit = 4,
            /// <summary>
            /// 删除
            /// </summary>
            [Description("删除")]
            Delete = 8,
            /// <summary>
            /// 打印
            /// </summary>
            [Description("打印")]
            Print = 16,
            /// <summary>
            /// 导出
            /// </summary>
            [Description("导出")]
            Export = 32,

        }

        /// <summary>
        /// TreeListType       
        /// </summary>
        public enum TreeListType
        {
            /// <summary>
            /// 空
            /// </summary>
            Null = 0,
            /// <summary>
            /// 科室
            /// </summary>
            [Description("科室")]
            Org,
            /// <summary>
            /// 产品类别
            /// </summary>
            [Description("产品类别")]
            ProductCategory,
            /// <summary>
            /// 产品
            /// </summary>
            [Description("产品")]
            Product,
            /// <summary>
            /// 设备
            /// </summary>
            [Description("设备")]
            Device,
            /// <summary>
            /// 仓库
            /// </summary>
            [Description("仓库")]
            Warehouse,
            /// <summary>
            /// 库位
            /// </summary>
            [Description("库位")]
            Location,
            /// <summary>
            /// 网篮
            /// </summary>
            [Description("网篮")]
            Container,


            /// <summary>
            /// 区域
            /// </summary>
            [Description("区域")]
            Zone,

            /// <summary>
            /// 组别
            /// </summary>
            [Description("组别")]
            ArchiveGroup,

            /// <summary>
            /// 消息中心
            /// </summary>
            [Description("消息中心")]
            MessageCenter,
        }



        /// <summary>
        /// 通用窗体选择数据的枚举
        /// </summary>
        public enum SelectDataType
        {
            /// <summary>
            /// 科室
            /// </summary>
            Org = 0,
            /// <summary>
            /// 用户
            /// </summary>
            User = 1,
            /// <summary>
            /// 生产商
            /// </summary>
            Manufacture = 2,
            /// <summary>
            /// 供应商
            /// </summary>
            Supplier = 3,
            /// <summary>
            /// 网篮
            /// </summary>
            Container = 4,
            /// <summary>
            /// 产品
            /// </summary>
            Product = 5,
            /// <summary>
            /// 产品类别
            /// </summary>
            ProductCategory = 6,
            /// <summary>
            /// 包等级
            /// </summary>
            CostLevel = 7,
            /// <summary>
            /// 仓库
            /// </summary>
            Warehouse = 8,
            /// <summary>
            /// 库位
            /// </summary>
            Location = 9,
            /// <summary>
            /// 设备
            /// </summary>
            DeviceName = 10,
            /// <summary>
            /// 设备预设程序
            /// </summary>
            ProcessRule = 11,
            /// <summary>
            /// 角色
            /// </summary>
            Role = 12,
            /// <summary>
            /// 病人
            /// </summary>
            Patient = 13,

            /// <summary>
            /// 档案组别（供应室）
            /// </summary>
            AcGroup = 14,

            /// <summary>
            /// 区域
            /// </summary>
            Zone = 15,

            /// <summary>
            /// 包装属性
            /// </summary>
            PackingAtrr = 15,
        }

        /// <summary>
        /// 日期枚举
        /// </summary>
        public enum DateType
        {
            Yesterday = -1,
            Today = 0,
            Week = 1,
            Month = 2,
            Year = 3,
            All = 4
        }

        /// <summary>
        /// NavBar 枚举 (无navBar显示效果则参数是null)
        /// </summary>
        public enum NavBarGroup
        {
            /// <summary>
            /// 产品类别
            /// </summary>
            [Description("产品类别")]
            ProductCategory = 1,
            /// <summary>
            /// 产品
            /// </summary>
            [Description("产品")]
            Product = 2,
            /// <summary>
            /// 设备
            /// </summary>
            [Description("设备")]
            Device = 3,

            /// <summary>
            /// 设备
            /// </summary>
            [Description("网篮")]
            Container = 4,
            /// <summary>
            /// 空的
            /// </summary>
            Null = -1,
        }



        /// <summary>
        /// 有效期类型
        /// </summary>
        public enum ValidateType
        {
            天 = 0,
            月 = 1
        }

        /// <summary>
        /// 自定义报表 选择条件控件类型
        /// </summary>
        public enum ReportFilterValueType
        {
            文本框 = 0,
            Button选择框取ID = 1,
            Button选择框取文本 = 2,
            时间范围 = 4,
            列表勾选值可多选 = 3,
            列表选值 = 5,
        }

        /// <summary>
        /// 自定义条件控件返回值类型
        /// </summary>
        public enum ReportFilterReturnValueType
        {
            字符 = 0,
            bool值 = 1,
            整形列表 = 2,
            整形值 = 3
        }
        /// <summary>
        /// 自定义报表 升序降序
        /// </summary>
        public enum ReportSortType
        {
            升序 = 1,
            降序 = 2
        }
        /// <summary>
        /// 入库类型
        /// </summary>
        public enum StockIntoType
        {
            /// <summary>
            /// 采购
            /// </summary>
            [Description("采购")]
            Purchase = 1,

            /// <summary>
            /// 借用
            /// </summary>
            [Description("借用")]
            Borrow = 2,

            /// <summary>
            /// 其他
            /// </summary>
            [Description("其他")]
            Other = 3,
        }
        /// <summary>
        /// 出库类型
        /// </summary>
        public enum StockOutType
        {
            /// <summary>
            /// 使用
            /// </summary>
            [Description("使用")]
            Used = 0,

            /// <summary>
            /// 损耗
            /// </summary>
            [Description("损耗")]
            Attrition = 1,

            /// <summary>
            /// 过期
            /// </summary>
            [Description("过期")]
            Expired = 2,

            /// <summary>
            /// 报废
            /// </summary>
            [Description("报废")]
            Scrap = 3,

            /// <summary>
            /// 归还
            /// </summary>
            [Description("归还")]
            Return = 4,

            /// <summary>
            /// 退货
            /// </summary>
            [Description("退货")]
            Refund = 5,

            /// <summary>
            /// 发放
            /// </summary>
            [Description("发放")]
            Provide = 6,
        }

        /// <summary>
        /// 审核结果
        /// </summary>
        public enum VerifyResult
        {
            [Description("合格")]
            合格 = 1,

            [Description("不合格")]
            不合格 = 2,

            [Description("部分合格")]
            部分合格 = 3,

        }

        /// <summary>
        /// 器械缺失原因
        /// </summary>
        public enum InstrumentLostReason
        {
            [Description("使用")]
            使用 = 1,

            [Description("损坏")]
            损坏 = 2,

            [Description("丢失")]
            丢失 = 3,

        }
        /// <summary>
        /// 急件包等级
        /// </summary>
        public enum DispatchLevel
        {
            [Description("普通")]
            普通 = 1,

            [Description("急件")]
            急件 = 2,

            [Description("特急")]
            特急 = 3,

        }
        public enum CacheKey
        {
            #region 111
            [Description("空")]
            NULL = 0,

            [Description("组别")]
            ArchiveGroup = 1,

            [Description("生物监测不合格原因")]
            BPCDFailureReason = 2,

            [Description("容器种类")]
            ContainerType = 3,

            [Description("化学监测不合格原因")]
            CPCDFailureReason = 4,

            [Description("设备类型")]
            DeviceType = 5,

            [Description("清洗消毒不合格原因")]
            DisinfectFailureReason = 6,

            [Description("急件包等级")]
            DispatchLevel = 7,

            [Description("流程状态")]
            FlowStatus = 8,

            [Description("流程种类")]
            FlowType = 9,

            [Description("器械缺失原因")]
            InstrumentLostReason = 10,

            [Description("产品类型(大类)")]
            ProductType = 11,

            [Description("灭菌不合格原因")]
            SterilizationFailureReason = 12,

            [Description("入库类型")]
            StockIntoType = 13,

            [Description("出库类型")]
            StockOutType = 14,


            [Description("库存产品状态")]
            StockProductStatus = 15,

            [Description("TenantType")]
            TenantType = 16,

            [Description("清洗消毒不合格原因")]
            TypeCode = 17,

            [Description("审核结果")]
            VerifyResult = 18,
            #endregion
            [Description("审核类型")]
            VerifyType = 19,

            [Description("清洗类型")]
            WashType = 20,

            [Description("产品类别(小类)")]
            ProductCategory = 21,

            [Description("产品")]
            Product = 22,

            [Description("科室")]
            Organazation = 23,

            [Description("仓库")]
            Warehouse = 24,

            [Description("区域")]
            Zone = 25,

            [Description("区域")]
            Location = 26,

            [Description("生产商")]
            Manufacturer = 27,

            [Description("供应商")]
            Supplier = 28,

            [Description("医院")]
            Hospital = 29,

            [Description("容器")]
            Container = 30,

            [Description("包装属性")]
            PackingAttr = 31,

            [Description("产品种类和类型")]
            ProductCategoryAndType = 32,

            [Description("设备预设程序")]
            DeviceProcessRule = 33,

            [Description("设备")]
            Device = 34,
            [Description("产品属性")]
            ProductAttr = 35,

            [Description("角色")]
            Role = 36,
            [Description("产品管理方式")]
            ProductManagementStyle = 37,

            [Description("性别")]
            Sex = 38,

            [Description("在职状态")]
            InJobState = 39,

            [Description("库位类型")]
            LocationType = 40,

            [Description("维修类型")]
            MaintainType = 41,

            [Description("用户")]
            User = 42,
            [Description("灭菌类型")]
            SterilizeType = 43,

            [Description("灭菌程序")]
            SterilizeProgram = 44,

            [Description("清洗程序")]
            WashProgram = 45,

        }
        /// <summary>
        /// 设备类型
        /// </summary>
        public enum DeviceType
        {
            /// <summary>
            /// 人工清洗
            /// </summary>
            ManualDisinfect = 1,
            /// <summary>
            /// 清洗消毒器
            /// </summary>
            Disinfector = 2,
            /// <summary>
            ///  超声波清洗器 
            /// </summary>
            SupersonicDisinfector = 3,

            /// <summary>
            /// 烘干器
            /// </summary>
            Dryer = 4,

            /// <summary>
            /// 封口机
            /// </summary>
            Sealer = 5,

            /// <summary>
            /// 压力蒸汽灭菌器
            /// </summary>
            PressureSteamSterilizer = 6,

            /// <summary>
            /// 等离子灭菌器
            /// </summary>
            PlasmaSterilizer = 7,

            /// <summary>
            /// 环氧乙烷灭菌器
            /// </summary>
            EthyleneOxideSterilizer = 8,

            /// <summary>
            /// 强光检查器
            /// </summary>
            LightChecker = 9,
        }


        /// <summary>
        /// 审核类型
        /// </summary>
        public enum VerifyType
        {

            [Description("未设定")]
            NotSet = 0,


            [Description("清洗消毒审核")]
            DisinfectVerify = 1,


            [Description("配包审核")]
            PackVerify = 2,


            [Description("灭菌审核")]
            SterilizationVerify = 3,


            [Description("灭菌-BD监测审核")]
            SterilizationBDVerify = 4,


            [Description("灭菌-化学监测审核")]
            SterilizationCpcdVerify = 5,

            [Description("灭菌-生物监测审核")]
            SterilizationBpcdVerify = 6,

            [Description("开包审核")]
            OpenPackageVerify = 7,

        }

        /// <summary>
        /// 产品管理方式
        /// </summary>
        public enum ProductManagementStyle
        {
            /// <summary>
            /// 未设定
            /// </summary>
            NotSet = 0,

            /// <summary>
            /// 批次管理
            /// </summary>
            Lot,

            /// <summary>
            /// 单品管理
            /// </summary>
            Single,

            /// <summary>
            /// 单品管理且带有追踪码
            /// </summary>
            SingleWithTraceCode,

        }

    }
}







