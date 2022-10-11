using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tea;
using Tea.Utils;

namespace BLL
{
    public class SendSms
    {
        public static AlibabaCloud.SDK.Dysmsapi20170525.Client CreateClient()
        {
            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config
            {
                // 您的 AccessKey ID
                AccessKeyId = "LTAI4FsX5yKZFr6aqwgcRXeY",
                // 您的 AccessKey Secret
                AccessKeySecret = "yxDKY4kwcVLbozHbmTL3ZG2IYcvjTy",
            };
            // 访问的域名
            config.Endpoint = "dysmsapi.aliyuncs.com";
            return new AlibabaCloud.SDK.Dysmsapi20170525.Client(config);
        }

        public string send(string phoneNumber, string menuName, int yujingValue, int Dvalue, int lifeValue)
        {
            string reslut;
            AlibabaCloud.SDK.Dysmsapi20170525.Client client = CreateClient();
            AlibabaCloud.SDK.Dysmsapi20170525.Models.SendSmsRequest sendSmsRequest = new AlibabaCloud.SDK.Dysmsapi20170525.Models.SendSmsRequest
            {
                PhoneNumbers = phoneNumber,
                SignName = "工业助理",
                TemplateCode = "SMS_246010516",
                TemplateParam = "{'name':'" + menuName + "','value':" + yujingValue + ",'dvalue':" + Dvalue + ",'liftValue':'" + lifeValue + "'}",
            };
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                // 复制代码运行请自行打印 API 的返回值
                client.SendSmsWithOptions(sendSmsRequest, runtime);
                reslut = "成功";
            }
            catch (TeaException error)
            {
                // 如有需要，请打印 error
                AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
                reslut = "发送失败";
            }
            catch (Exception _error)
            {
                TeaException error = new TeaException(new Dictionary<string, object>
                {
                    { "message", _error.Message }
                });
                // 如有需要，请打印 error
                AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
                reslut = "发送失败";
            }
            return reslut;
        }
    }
}
