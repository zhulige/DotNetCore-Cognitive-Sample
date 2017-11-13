# DotNetCore-Cognitive-Sample

DotNetCore-Cognitive-Sample 是 .Net Core 编写的 Azure 认知服务示例代码集。

##CustomVision_Prediction_Sample

CustomVision_Prediction_Sample 是 Azure 自定义影像服务示例代码。

官方代码：https://docs.microsoft.com/zh-cn/azure/cognitive-services/custom-vision-service/use-prediction-api

预览站点：https://www.customvision.ai

为了测试方便在官方代码上做了一下优化。
 - 支持 Key 的传入
 - 支持 Url 的传入
 - 支持 图片目录 的传入
 - 支持 结果 Log 的输出（用分号做了分割，易于导入Excel或数据库）

运行
```
dotnet CustomVision_Prediction_Sample.dll key url
dotnet CustomVision_Prediction_Sample.dll a3ab69857dbe48818e71a6aa7267a6f6 https://southcentralus.api.cognitive.microsoft.com/customvision/v1.0/Prediction/421ea876-3749-4972-9d2b-95467ecfff83/image?iterationId=921dc745-8b8b-4ccd-bce4-1b47312c0baa
Enter image file path: d:\images
```
或
```
dotnet CustomVision_Prediction_Sample
Enter key: a3ab69857dbe48818e71a6aa7267a6f6
Enter url: https://southcentralus.api.cognitive.microsoft.com/customvision/v1.0/Prediction/421ea876-3749-4972-9d2b-95467ecfff83/image?iterationId=921dc745-8b8b-4ccd-bce4-1b47312c0baa
Enter image file path: d:\images
```
启动后输入 key url 图片目录地址
