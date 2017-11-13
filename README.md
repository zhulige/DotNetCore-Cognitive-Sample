# DotNetCore-Cognitive-Sample
DotNetCore-Cognitive-Sample 是 .Net Core 编写的 Azure 认知服务示例代码集。

##CustomVision_Prediction_Sample
CustomVision_Prediction_Sample 是 Azure 自定义影像服务示例代码。
官方代码：https://docs.microsoft.com/zh-cn/azure/cognitive-services/custom-vision-service/use-prediction-api
预览站点：https://www.customvision.ai

为了测试方便在官方代码上做了一下优化。
 - 支持 Key 的传入
 - 支持 url 的传入
 - 支持 图片目录 的传入
 - 支持 结果 Log 的输出（用分号做了分割，易于导入Excel或数据库）

运行
dotnet CustomVision_Prediction_Sample.dll key url
或
dotnet CustomVision_Prediction_Sample
启动后输入 key url 图片目录地址
