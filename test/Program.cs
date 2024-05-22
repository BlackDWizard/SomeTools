using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Digests;
using System.Data.SqlClient;
using System.Net.Mail;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;
//using iTextSharp.text;
//using iTextSharp.text.html.simpleparser;
//using iTextSharp.text.pdf;
using System.Linq;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            //string cc = "{\"tx_id\":\"85343268-2d9e-4163-9c92-63fad5f3d104\",\"data\":\"{\\\"pid\\\":\\\"A127195465\\\",\\\"holder\\\":\\\"\\\",\\\"birthday\\\":\\\"\\\",\\\"email\\\":\\\"\\\",\\\"mobile\\\":\\\"0911111111\\\",\\\"salt\\\":\\\"75a740b1-91d1-4736-8491-ab719f0b4abd\\\",\\\"as_optional\\\":\\\"\\\"}\",\"pkcs7\":\"[\\\"MIIIuwYJKoZIhvcNAQcCoIIIrDCCCKgCAQExDzANBglghkgBZQMEAgEFADCBnwYJKoZIhvcNAQcBoIGRBIGOeyJwaWQiOiJBMTI3MTk1NDY1IiwiaG9sZGVyIjoiIiwiYmlydGhkYXkiOiIiLCJlbWFpbCI6IiIsIm1vYmlsZSI6IjA5MTExMTExMTEiLCJzYWx0IjoiNzVhNzQwYjEtOTFkMS00NzM2LTg0OTEtYWI3MTlmMGI0YWJkIiwiYXNfb3B0aW9uYWwiOiIifaCCBkMwggY/MIIEJ6ADAgECAgRfe6dCMA0GCSqGSIb3DQEBCwUAMHQxCzAJBgNVBAYTAlRXMRswGQYDVQQKExJUQUlXQU4tQ0EuQ09NIEluYy4xIDAeBgNVBAsTF1VzZXIgQ0EtRXZhbHVhdGlvbiBPbmx5MSYwJAYDVQQDEx1UV0NBIFRlc3QgSW5mb3JtYXRpb24gVXNlciBDQTAeFw0yMjEyMTIwMzM0MjZaFw0yMjEyMjcxNTU5NTlaMIGwMQswCQYDVQQGEwJUVzEQMA4GA1UEChMHRmluYW5jZTEmMCQGA1UECxMdVFdDQSBUZXN0IEluZm9ybWF0aW9uIFVzZXIgQ0ExITAfBgNVBAsTGDcwNzU5MDI4LVJBLU9QRU5DRVJUUE1DMzEYMBYGA1UECxMPUkEtT1BFTkNFUlRQTUMzMSowKAYDVQQDEyFBMTI3MTk1NDY1LTAwLTAwLTQ2NDc5MTAzOjpIV0MwMDAwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQDS+TXpPCarEJ31guG8prxz7GHXWswhNJhuO4AlthAkEFw22VeZDdUShDAmtY+GYnhR9m4S5zIn3xv3wlucl6rxTZ9j1Gq0rXjpE+k1V4b1y7Q4UaUgZ2+m7T351oNlHkfka+PzZ54CRXt1Kkeq2uVWafsUn7D4jskKMwMxq++MZct8NinBKsvhk4iq1gnybWq3V5/dwyVSadHjXMtp6i4eHDdK4Y9lHCWekWxxYkKu29qJaJYlgttQpuj2D1ON5xUOU+YtddVAil0hbkwQHXXXThkOLdRTGFACxpQSm9bXegTaxfH7nRXc9c+2zek7FM6lZ1i5EQz3FJrI0G7aw3MLAgMBAAGjggGaMIIBljArBgNVHSMEJDAigCAG16Cizh0+yC08pCfkOdS2qOp1xSEadFBpXaq+EH55ijApBgNVHQ4EIgQgCClmkiEasqAndGqvYv2H/Cgw1TUtNDudMvblzisc5f4wUQYDVR0fBEowSDBGoESgQoZAaHR0cDovL2l0YXgudHdjYS5jb20udHcvdGVzdGNybC9UZXN0X3htbHVjYV9yZXZva2VfU2hhMl8yMDE3LmNybDANBgNVHREEBjAEgQJAQDA/BggrBgEFBQcBAQQzMDEwLwYIKwYBBQUHMAGGI2h0dHA6Ly9PQ1NQX0V2YWwuVGFpY2EuY29tLnR3OjgwMDEvMF8GA1UdIARYMFYwVAYHYIEeAwEIBTBJMCMGCCsGAQUFBwIBFhdodHRwOi8vd3d3LnR3Y2EuY29tLnR3LzAiBggrBgEFBQcCAjAWGhRSZXN0cmljdGlvbiA9My4yLjIuMzAJBgNVHRMEAjAAMA4GA1UdDwEB/wQEAwID+DAdBgNVHSUEFjAUBggrBgEFBQcDAgYIKwYBBQUHAwQwDQYJKoZIhvcNAQELBQADggIBACiRXBf87W1dlWTydnMjfLxOT3UZuJoMVSOZjTX4vOOOoGjhWqxJvw9+6ipuMopJwUa5WCfeSqYVmU/5vHoaduNjTL/IbiDs5eoM+2rUAt4GIAyHYDD5XosCCf2caUuZxqb2phzyATUWz2thN4Zf14KLdG1iXG5ZlFOJG1P7264w/6hJzkuXGes9FwinceoBXJrtCGYQU67phKJt0pVq8XEhUNBixGbk2wf6AxM7W0psBKycvYg/kc+CoZPQlKJxmm0+rK62lACdPVaX1GK0pRXR6vPQ2SKkl01+LLNXo/khch99fO1yVxamn67o99QUKz6toaLIIfaSQ1CgnKymWogOIRM8TC6CxpxTEM/6J30A5DOPEtxe7hOTl4FrtQJfnoDnE5xOj1ThXpCTW8WyPdQf70QqMB1viR3QR33xqcFxFsqO7YACAcYfuw4MDCpRy0kugg8UMQAf66SeXx4rQk8wiFLPG7c0XaUdDmq0v3L9KCLbClN4Nd/i622AbGc9BpVaOMP1qRNPMgwRI7FFV9E+PhQK8BDnKDWR3hlsoMo8H8NCbUIjphH7p4nZbeNJBrhvHzx9uun3fvdshnXaoBWKTXTMtY0+/O3J8R462hDcvfkqcEGJMnD6j9PqcZUMc99iAsiZjpmq27kvbNfra3xH161X9qt20JVlKEjMoM3+MYIBpzCCAaMCAQEwfDB0MQswCQYDVQQGEwJUVzEbMBkGA1UEChMSVEFJV0FOLUNBLkNPTSBJbmMuMSAwHgYDVQQLExdVc2VyIENBLUV2YWx1YXRpb24gT25seTEmMCQGA1UEAxMdVFdDQSBUZXN0IEluZm9ybWF0aW9uIFVzZXIgQ0ECBF97p0IwDQYJYIZIAWUDBAIBBQAwDQYJKoZIhvcNAQEBBQAEggEAha5zbleSG8RATJm9YDOGr2kIitGkw4CWGfXlT5y3C5dj7jwenu36A2GH5V9kljRfhypZ/RVV6bRTQw+z1eac/SWXiKS0krOJ+86n7jU4OmoJe11TbrfAE2mT2gR5CcvhARyG1thX9hSwPbdGTrmQu9QpYpl4F4d5eNNMrMfjP3+kcquPpFfdr0OVG5TgsRyAYGvEFiEqU0GgwxKY5I3mH7jzPvQ26aZ3eh6Sf68qwhgPq0DM//Sq9vEMVT7VQsLQg5Q/QTO7n6rz9ZhM9Ka02TCJecftMxkbNQt0jhqvrdaATng6soOIb09TtE/HV3IT4XoBO4KN8RE24QQ+KcmZpg==\\\"]\",\"verification_type\":\"FCS\",\"as_id\":\"twid\"}";
            //string bb = "{\"data\":{\"id\":\"3a087410-b9fc-510b-f10f-a2bf73a98e33\",\"status\":\"Pending\",\"code\":\"IDP101\",\"source\":\"System\",\"verification\":{\"documentType\":\"IDCARD\",\"documentCountry\":\"TWN\",\"details\":{\"idNumber\":\"L122328052\",\"dateOfBirth\":\"1977-09-12\",\"dateOfIssue\":\"2012-08-03\",\"gender\":\"male\",\"givenName\":\"佳閔\",\"issueCity\":\"新北市\",\"issueReason\":\"換發\",\"name\":\"蔡佳閔\",\"surname\":\"蔡\",\"address\":\"新北市泰山區山腳里11鄰泰林路二段470號十三樓\",\"father\":\"蔡基礎\",\"militaryService\":\"常兵備役\",\"mother\":\"蔡陳朱鑾\",\"placeOfBirth\":\"臺灣省臺中縣\",\"serialNumber\":\"0033546310\",\"spouse\":\"曾宜婷\"}}},\"type\":\"IdentityVerification.Identification\",\"customerId\":\"web_client_1672381650427\",\"timestamp\":1672384647617,\"event\":\"TW_CCPL_default\"}";
            //string ee = "{\"SEND-DATA-VSS885CS\":{\"REPLY-CODE\":\"0\",\"REPLY-MSG\":\"AAAAAAA==\",\"SEND-PASSWORD\":{\"WS-TEL\":\"53287\",\"WS-MGR\":\"0\",\"FUNCTION-CODE\":\"1\",\"WS-CHOICE\":\"0\",\"WS-NAME\":\"AAA==\"},\"WS-FIX-DATA-A:\":{\"WS-IDNO\":\"M137778770\",\"WS-ENGLISH-NAME\":\"CHANG SAN\",\"WS-BRNID\":\"0\",\"WS-DATE\":\"0\",\"WS-ACNT-SEQ\":\"0\",\"WS-USECNT\":\"0\",\"WS-ICSEQ\":\"0\",\"WS-APP-SEQ\":\"0\",\"WS-BIRTH\":\"1607\",\"WS-APP-DATE\":\"0\",\"WS-CARD-STATUS\":\"0\",\"WS-MAKE-FLAG\":\"4\",\"WS-TYPE\":\"V\",\"WS-O-DEPACNO\":{\"WS-O-DEPACNO\":\"12020\",\"WS-O-ACNO\":\"83051\"},\"WS-IN-BANK-ACNO\":{\"WS-IN-BANK-ACNO-1\":[{\"WS-I-BANK-NO\":\"052\",\"WS-I-DEPACNO\":\"123456789\"},{\"WS-I-BANK-NO\":\"052\",\"WS-I-DEPACNO\":\"222222222\"},{\"WS-I-BANK-NO\":\"008\",\"WS-I-DEPACNO\":\"987654321\"},{\"WS-I-BANK-NO\":\"052\",\"WS-I-DEPACNO\":\"12342323232\"},{\"WS-I-BANK-NO\":\"052\",\"WS-I-DEPACNO\":\"2342354253425\"},{\"WS-I-BANK-NO\":\"008\",\"WS-I-DEPACNO\":\"3242342310\"},{\"WS-I-BANK-NO\":\"008\",\"WS-I-DEPACNO\":\"032423432432\"},{\"WS-I-BANK-NO\":\"052\",\"WS-I-DEPACNO\":\"3454350345435435\"}]},\"WS-VISA-CARD-NO\":\"\",\"WS-OLYMPIC-FLAG\":\"\",\"WS-BLOCK-FLG\":\"\",\"WS-GET-BRN\":\"999\",\"WS-GET-CARD-BRN\":\"999\",\"WS-PER-EMPNO\":\"0\"},\"WS-FIX-DATA-B\":{\"WS-DAILY-LIMIT\":\"50000\",\"WS-NONAGR-FLG\":\"Y\",\"WS-NAT-FLG\":\"Y\",\"WS-CELLPHONE-NO\":\"\"}}}";
            //string gg = "{\"SalesName\":\"羅淑勤\",\"TokenFlag\":true,\"ErrMsg\":\"\",\"Success\":true}";
            string ff = "{\"AGROPENJSON\":{\"WS-IDNO\":\"K221138305\",\"WS-EMPNO-7-TEL\":\"1251550\",\"WS-TELLER-5\":\"1603\",\"WS-EMPNO-7-MGR\":\"0\",\"WS-MGR-5\":\"0\",\"WS-IVRA-CHANNEL\":\"1\",\"WS-IVRA-FUNCTION-CODE\":\"1\",\"WS-IVRA-COUNT-TOTAL\":\"3\",\"WS-IVRA-ADD-DEL-COUNT\":\"0\",\"WS-AGR-DATA\":[{\"WS-IVRA-FLG-KIND\":\"I1\",\"WS-IVRA-KIND\":\"\",\"WS-IVRA-BANK\":\"52\",\"WS-IVRA-DEPACNO-OUT\":\"6921000001988\"},{\"WS-IVRA-FLG-KIND\":\"I3\",\"WS-IVRA-KIND\":\"\",\"WS-IVRA-BANK\":\"52\",\"WS-IVRA-DEPACNO-OUT\":\"12005300021819\"},{\"WS-IVRA-FLG-KIND\":\"I2\",\"WS-IVRA-KIND\":\"\",\"WS-IVRA-BANK\":\"806\",\"WS-IVRA-DEPACNO-OUT\":\"9376343798541009\"}],\"WS-IVRA-REPLY-CODE\":\"00\",\"WS-IVRA-MSG\":\"0\"}}";
            //JsonDeserial(ff);

            #region UID
            List<string> dd = new List<string>();
            dd.Add("07394654");
            dd.Add("33608891");
            dd.Add("45703808");
            dd.Add("24237384");
            dd.Add("22059861");
            dd.Add("24491245");
            dd.Add("77662576");
            dd.Add("28899743");
            dd.Add("94452955");
            dd.Add("30501277");
            dd.Add("94850721");
            dd.Add("72696175");
            dd.Add("53307470");
            dd.Add("86894936");
            dd.Add("96642116");
            dd.Add("61184970");
            dd.Add("46536770");
            dd.Add("78738434");
            dd.Add("61261651");
            dd.Add("77925661");
            dd.Add("13198645");
            dd.Add("60098344");
            dd.Add("31079207");
            dd.Add("56715611");
            dd.Add("03096373");
            dd.Add("51057080");
            dd.Add("01208576");
            dd.Add("13770847");
            dd.Add("00616420");
            dd.Add("21053474");
            dd.Add("53628311");
            dd.Add("82289563");
            dd.Add("96337853");
            dd.Add("83235427");
            dd.Add("92770119");
            dd.Add("19410074");
            dd.Add("44432723");
            dd.Add("93077170");
            dd.Add("26606236");
            dd.Add("57028458");
            dd.Add("20771032");
            dd.Add("69460008");
            dd.Add("49663309");
            dd.Add("35099669");
            dd.Add("03337289");
            dd.Add("71141939");
            dd.Add("30527635");
            dd.Add("59586767");
            dd.Add("43201604");
            dd.Add("29629867");
            dd.Add("46272779");
            dd.Add("42175641");
            dd.Add("00676480");
            dd.Add("65966722");
            dd.Add("46716226");
            dd.Add("11482150");
            dd.Add("99434078");
            dd.Add("20506766");
            dd.Add("69861860");
            dd.Add("16262492");
            dd.Add("87543570");
            dd.Add("45677858");
            dd.Add("35662510");
            dd.Add("21983956");
            dd.Add("88535367");
            dd.Add("27757513");
            dd.Add("24750571");
            dd.Add("52263263");
            dd.Add("04247407");
            dd.Add("06050836");
            dd.Add("46289835");
            dd.Add("50062536");
            dd.Add("29856454");
            dd.Add("20563652");
            dd.Add("07713138");
            dd.Add("64587724");
            dd.Add("91227189");
            dd.Add("93665121");
            dd.Add("70002327");
            dd.Add("54719492");
            dd.Add("14876761");
            dd.Add("75889176");
            dd.Add("27003894");
            dd.Add("36209159");
            dd.Add("27003774");
            dd.Add("77215232");
            dd.Add("40115078");
            dd.Add("58671441");
            dd.Add("65430290");
            dd.Add("64209877");
            dd.Add("69283947");
            dd.Add("07076779");
            dd.Add("21885961");
            dd.Add("80326036");
            dd.Add("70267939");
            dd.Add("60951072");
            dd.Add("04972358");
            dd.Add("99940573");
            dd.Add("27200678");
            dd.Add("63915919");

            int ddd = 1;
            //foreach (string d in dd)
            //{
            //    if (UnifiedNumberingCheck(d))
            //        Console.WriteLine(ddd.ToString() + " : pass");
            //    else
            //        Console.WriteLine(ddd.ToString() + " : np");
            //    ddd++;
            //}
            #endregion

            #region 中文字轉Unicode
            //byte[] bytes = Encoding.Unicode.GetBytes(bb);
            //var stringBuilder = new StringBuilder();

            //var character = "一".ToCharArray();
            //var ascii = "";
            //for (var i = 0; i < character.Length; i++)
            //{
            //    var code = character[i];
            //    //for(var j = 0; j < code.Length; j++)
            //    //{
            //    if (code >= 19968 && code <= 40959)
            //    {
            //        var charAscii = Convert.ToInt32(character[i]).ToString("X");
            //        ascii += "\\u" + charAscii;
            //    }
            //    else
            //    {
            //        ascii += character[i];
            //    }
            //    //}
            //}
            #endregion

            #region 憑證密碼
            //string origin1 = "A133147439A9";
            //Console.WriteLine("原文 : " + origin1);
            //byte[] tmp1 = Encoding.UTF8.GetBytes("A1331474390770707");

            ////Console.WriteLine(Convert.ToBase64String(key));
            //string aesaaa1 = Aes256Encode(tmp1, origin1);
            //Console.WriteLine("AES : " + aesaaa1);
            //SHA256Managed sha256 = new SHA256Managed();
            //byte[] sha256aesaaa1 = sha256.ComputeHash(Encoding.UTF8.GetBytes(aesaaa1));

            //Console.WriteLine("SHA256 : " + BitConverter.ToString(sha256aesaaa1).Replace("-", "").ToLower());//
            //Console.WriteLine("SHA256(50) : " + BitConverter.ToString(sha256aesaaa1).Replace("-", "").ToLower().Substring(0, 50));//

            //string certpw1 = assignCertPasswordEncrypt("A133147439", BitConverter.ToString(sha256aesaaa1).Replace("-", "").ToLower().Substring(0, 50));
            //Console.WriteLine("最終結果：" + certpw1);

            //Console.WriteLine("最終結果還原SHA256(50) : " + assignCertPasswordDecrypt("A133147439", "ESQvGebAXZ7zADccW7H4c6+OKzgp9mG80QpdHCR4/fJTOKk73HB7ciNYcMzOCGAb6EjUsONnx0OF7ZrqMmPeOA=="));

            //string bbb1 = Aes256Decode(tmp1, aesaaa1);
            //Console.WriteLine("解出原文 : " + bbb1 + "\r\n");


            //string origin2 = "A234567890A0";
            //Console.WriteLine("原文 : " + origin2);
            //byte[] tmp2 = Encoding.UTF8.GetBytes("A2345678900880808");

            ////Console.WriteLine(Convert.ToBase64String(key));
            //string aesaaa2 = Aes256Encode(tmp2, origin2);
            //Console.WriteLine("AES : " + aesaaa2);
            //byte[] sha256aesaaa2 = sha256.ComputeHash(Encoding.UTF8.GetBytes(aesaaa2));

            //Console.WriteLine("SHA256 : " + BitConverter.ToString(sha256aesaaa2).Replace("-", "").ToLower());//
            //Console.WriteLine("SHA256(50) : " + BitConverter.ToString(sha256aesaaa2).Replace("-", "").ToLower().Substring(0, 50));//
            //string certpw2 = assignCertPasswordEncrypt("A234567890", BitConverter.ToString(sha256aesaaa2).Replace("-", "").ToLower().Substring(0, 50));
            //Console.WriteLine("最終結果 : " + certpw2);
            //string ocertpw2 = assignCertPasswordDecrypt("A234567890", certpw2);
            //string bbb2 = Aes256Decode(tmp2, aesaaa2);
            //Console.WriteLine("解出原文 : " + bbb2);

            //Aes128Decode
            //Console.WriteLine("\\u9594");
            //Dictionary<string, object> firstLayerDic = new Dictionary<string, object>();
            //firstLayerDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(cc);


            //byte[] AESKey = UTF8Encoding.UTF8.GetBytes("KvloHv1DS3lMe53n" + "KvloHv1DS3lMe53n");

            //Console.WriteLine(Decrypt("oydjHoCTUqUp1LCWEhwQCIAhWPKqqO7UBhv811noEyA/zdk0ynqgh16/HEnmwYS/ZAkOBDFszC1LEOWKEXEQn4N/tLLUmdwIymGlhYzlPGR5+VdfUP6Jh3UWryEWcFV/50w6LJJgkMA87jyE6+kR9g==", AESKey, "cFJ0feEe4icTRx9P"));

            //string aa = BitConverter.ToString(AESKey).Replace("-", "");

            #endregion

            #region comment
            //Console.WriteLine(firstLayerDic["pkcs7"].ToString().Substring(2, firstLayerDic["pkcs7"].ToString().Length - 4));
            //base64test();
            //datetest();
            //emailReplace();
            //GetEnPWD("1234");
            //Console.WriteLine(SHA256_UTF16("{'BusinessNo':'46479103','ApiVersion':'1.0','HashKeyNo':'15','VerifyNo':'c8d5b064ea4b4f9ab040715f4b025758','ReturnURL':'https://www.asiavista.com.tw:5959/online-middle-server_e2e/pl?CTID=16F1973C-EF18-4B46-A838-97F7A9E331A7#/finishMydata','ReturnParams':'','IdentifyNo':'','InputParams':{'MemberNo':'A123456789','MemberNoType':'0','Action':'CERT,PKCS7','Plaintext':'A123456789','CAType':'','AssignCertPassword':'A123456789@d','ErrCodeType':'1','IsCpntProcess':'N','PKCS7InputParams':{'isURL':'N','isCAVerify':'N','isFCERT':'N','Contents':['MTIz']}}}"));
            //JsonDeserial("{\"BusinessNo\":\"46479103\",\"ApiVersion\":\"1.0\",\"HashKeyNo\":\"15\",\"VerifyNo\":\"8d6f457d66424a7aba643f5db659d800\",\"ReturnURL\":\"https://www.asiavista.com.tw:5959/online-middle-server_e2e/pl?CTID=16F1973C-EF18-4B46-A838-97F7A9E331A7#/finishMydata\",\"ReturnParams\":\"\",\"IdentifyNo\":\"\",\"InputParams\":{\"MemberNo\":\"A123456789\",\"MemberNoType\":\"0\",\"Action\":\"CERT,PKCS7\",\"Plaintext\":\"A123456789\",\"CAType\":\"\",\"AssignCertPassword\":\"A123456789%q\",\"ErrCodeType\":\"1\",\"IsCpntProcess\":\"N\",\"PKCS7InputParams\":{\"isURL\":\"N\",\"isCAVerify\":\"N\",\"isFCERT\":\"N\",\"Contents\":[]}}}");

            //string reg = "using Newtonsoft.Json;using System;using System.Collections.Generic;namespace Vista.Business{    [JsonObject(Title = 'AGROPENJSON')]    public partial class TxIB : TxRestBase    {        #region Init        public void Init()        {            this._WS_IDNO = '';            this._WS_EMPNO_7_TEL = '';            this._WS_TELLER_5 = '';            this._WS_EMPNO_7_MGR = '';            this._WS_MGR_5 = '';            this._WS_IVRA_CHANNEL = '';            this._WS_IVRA_FUNCTION_CODE = '';            this._WS_IVRA_COUNT_TOTAL = '';            this._WS_IVRA_ADD_DEL_COUNT = '';            this._WS_AGR_DATA = new List<WS_AGR_DATA_Item>();            this._WS_IVRA_REPLY_CODE = '';            this._WS_IVRA_MSG = '';        }        #endregion        #region Private Properties        private string _WS_IDNO;        private string _WS_EMPNO_7_TEL;        private string _WS_TELLER_5;        private string _WS_EMPNO_7_MGR;        private string _WS_MGR_5;        private string _WS_IVRA_CHANNEL;        private string _WS_IVRA_FUNCTION_CODE;        private string _WS_IVRA_COUNT_TOTAL;        private string _WS_IVRA_ADD_DEL_COUNT;        private List<WS_AGR_DATA_Item> _WS_AGR_DATA;        private string _WS_IVRA_REPLY_CODE;        private string _WS_IVRA_MSG;        #endregion        #region Public Properties        [JsonProperty(PropertyName = 'WE-IDNO')]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property        public string WS_IDNO { get { return _WS_IDNO; } set { _WS_IDNO = value; } }        [JsonProperty(PropertyName = 'WS-EMPNO-7-TEL')]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property        public string WS_EMPNO_7_TEL { get { return _WS_EMPNO_7_TEL; } set { _WS_EMPNO_7_TEL = value; } }        [JsonProperty(PropertyName = 'WS-TELLER-5')]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property        public string WS_TELLER_5 { get { return _WS_TELLER_5; } set { _WS_TELLER_5 = value; } }        [JsonProperty(PropertyName = 'WS-EMPNO-7-MGR')]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property        public string WS_EMPNO_7_MGR { get { return _WS_EMPNO_7_MGR; } set { _WS_EMPNO_7_MGR = value; } }        [JsonProperty(PropertyName = 'WS-MGR-5')]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property        public string WS_MGR_5 { get { return _WS_MGR_5; } set { _WS_MGR_5 = value; } }        [JsonProperty(PropertyName = 'WS-IVRA-CHANNEL')]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property        public string WS_IVRA_CHANNEL { get { return _WS_IVRA_CHANNEL; } set { _WS_IVRA_CHANNEL = value; } }        [JsonProperty(PropertyName = 'WS-IVRA-FUNCTION-CODE')]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property        public string WS_IVRA_FUNCTION_CODE { get { return _WS_IVRA_FUNCTION_CODE; } set { _WS_IVRA_FUNCTION_CODE = value; } }        [JsonProperty(PropertyName = 'WS-IVRA-COUNT-TOTAL')]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property        public string WS_IVRA_COUNT_TOTAL { get { return _WS_IVRA_COUNT_TOTAL; } set { _WS_IVRA_COUNT_TOTAL = value; } }        [JsonProperty(PropertyName = 'WS-IVRA-ADD-DEL-COUNT')]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property        public string WS_IVRA_ADD_DEL_COUNT { get { return _WS_IVRA_ADD_DEL_COUNT; } set { _WS_IVRA_ADD_DEL_COUNT = value; } }        [JsonProperty(PropertyName = 'WS-AGR-DATA')]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property        public List<WS_AGR_DATA_Item> WS_AGR_DATA { get { return _WS_AGR_DATA; } set { _WS_AGR_DATA = value; } }        [JsonProperty(PropertyName = 'WS-IVRA-REPLY-CODE')]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property        public string WS_IVRA_REPLY_CODE { get { return _WS_IVRA_REPLY_CODE; } set { _WS_IVRA_REPLY_CODE = value; } }        [JsonProperty(PropertyName = 'WS-IVRA-MSG')]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property        public string WS_IVRA_MSG { get { return _WS_IVRA_MSG; } set { _WS_IVRA_MSG = value; } }        #endregion    }    public class WS_AGR_DATA_Item    {        public WS_AGR_DATA_Item()        {            this.WS_IVRA_FLG_KIND = '';            this.WS_IVRA_KIND = '';            this.WS_IVRA_BANK = '';            this.WS_IVRA_DEPACNO_OUT = '';        }        //要做為List物件中的{ get; set; }必填        [JsonProperty(PropertyName = 'WS-IVRA-FLG-KIND')]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property        public string WS_IVRA_FLG_KIND { get; set; }        [JsonProperty(PropertyName = 'WS-IVRA-KIND')]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property        public string WS_IVRA_KIND { get; set; }        [JsonProperty(PropertyName = 'WS-IVRA-BANK')]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property        public string WS_IVRA_BANK { get; set; }        [JsonProperty(PropertyName = 'WS-IVRA-DEPACNO-OUT')]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property        public string WS_IVRA_DEPACNO_OUT { get; set; }    }}";
            //string pattern = "/(?<=\')\\w +/g";
            //Regex regex = new Regex(pattern);

            //MatchCollection matchCollection = regex.Matches(reg);
            //foreach(string match in matchCollection)
            //{
            //    Console.WriteLine(match);
            //}

            //List<TxTransferAccountOther_DataType> accountIVRCSOTTData = new List<TxTransferAccountOther_DataType>();

            //XmlDocument _xmlAccount = new XmlDocument();

            //_xmlAccount.LoadXml("<XmlDS><TransferAccount><Channel>2</Channel><TYPE>I2</TYPE><BankName>472-模擬行472</BankName><BankCode>472</BankCode><No>2343465476856786</No><Action></Action></TransferAccount><TransferAccount><Channel>2</Channel><TYPE>I2</TYPE><BankName>463-模擬行463</BankName><BankCode>463</BankCode><No>3465476856786976</No><Action></Action></TransferAccount><TransferAccount><Channel>2</Channel><TYPE>I2</TYPE><BankName>461-模擬行461</BankName><BankCode>461</BankCode><No>4234123412341243</No><Action></Action></TransferAccount><TransferAccount><Channel>2</Channel><TYPE>I2</TYPE><BankName>466-模擬行466</BankName><BankCode>466</BankCode><No>4678467456745678</No><Action></Action></TransferAccount><TransferAccount><Channel>2</Channel><TYPE>I3</TYPE><BankName>052-渣打銀行</BankName><BankCode>052</BankCode><No>00305330000082</No><Action></Action></TransferAccount><TransferAccount><Channel>3</Channel><TYPE>I2</TYPE><BankName>472-模擬行472</BankName><BankCode>472</BankCode><No>2343465476856786</No><Action>I</Action></TransferAccount><TransferAccount><Channel>3</Channel><TYPE>I2</TYPE><BankName>463-模擬行463</BankName><BankCode>463</BankCode><No>3465476856786976</No><Action>I</Action></TransferAccount><TransferAccount><Channel>3</Channel><TYPE>I2</TYPE><BankName>461-模擬行461</BankName><BankCode>461</BankCode><No>4234123412341243</No><Action>I</Action></TransferAccount><TransferAccount><Channel>3</Channel><TYPE>I2</TYPE><BankName>466-模擬行466</BankName><BankCode>466</BankCode><No>4678467456745678</No><Action>I</Action></TransferAccount></XmlDS>");
            //foreach (XmlNode _node in _xmlAccount.FirstChild.ChildNodes)
            //{
            //    if (_node["Action"].InnerText != "")
            //    {
            //        if (_node["TYPE"].InnerText == "I4") //外幣匯款約定轉入帳號
            //        {
            //            if (_node["Channel"].InnerText == "1") //電話語音
            //            {
            //                TxTransferAccountOther_DataType itemIDAccount = new TxTransferAccountOther_DataType();
            //                itemIDAccount.Account = Convert.ToString(_node["Account"].InnerText);
            //                itemIDAccount.AccountBankName = Convert.ToString(_node["AccountBankName"].InnerText);
            //                itemIDAccount.AccountName = Convert.ToString(_node["AccountName"].InnerText);
            //                itemIDAccount.AccountName2 = Convert.ToString(_node["AccountName2"].InnerText);
            //                itemIDAccount.AccountAdd = Convert.ToString(_node["AccountAdd"].InnerText);
            //                itemIDAccount.Currency = Convert.ToString(_node["Currency"].InnerText);
            //                itemIDAccount.IDType = Convert.ToString(_node["IDType"].InnerText);
            //                itemIDAccount.NationalType = Convert.ToString(_node["NationalType"].InnerText);
            //                itemIDAccount.SwiftCode = Convert.ToString(_node["SwiftCode"].InnerText);
            //                itemIDAccount.FLG_KIND = _node["TYPE"].InnerText;
            //                itemIDAccount.KIND = Convert.ToString(_node["Action"].InnerText);
            //                accountIVRCSOTTData.Add(itemIDAccount);

            //            }
            //        }
            //    }
            //}
            #endregion

            #region char
            // Get a list of invalid path characters.
            //char[] invalidPathChars = Path.GetInvalidPathChars();

            //Console.WriteLine("The following characters are invalid in a path:");
            //ShowChars(invalidPathChars);
            //Console.WriteLine();

            //// Get a list of invalid file characters.
            //char[] invalidFileChars = Path.GetInvalidFileNameChars();

            //Console.WriteLine("The following characters are invalid in a filename:");
            //ShowChars(invalidFileChars);
            #endregion

            #region XML轉String比對
            //string xml = @"<Address><Nation>TW</Nation><County>undefined</County><District>undefined</District><Free>No. 45, Aly. 4, Ln. 23, Daan St., Yuanlin City, Changhua County, Taiwan (R.O.C.)</Free><NationName>TAIWAN  台灣</NationName></Address>";
            //string add = @"No. 45, Aly. 4, Ln. 23, Daan St., Yuanlin City, Changhua County, Taiwan (R.O.C.)";

            //string Nation;
            //string County;
            //string District;
            //string Free;

            //XmlDocument xmldoc = new XmlDocument();
            //xmldoc.Load(new StringReader(xml));
            //if (xmldoc.GetElementsByTagName("Nation").Count != 0)
            //    Nation = xmldoc.GetElementsByTagName("Nation")[0].InnerText;
            //County = xmldoc.GetElementsByTagName("County")[0].InnerText;
            //District = xmldoc.GetElementsByTagName("District")[0].InnerText;
            //Free = xmldoc.GetElementsByTagName("Free")[0].InnerText;

            //string freer = (Free.Replace(" ", ""));
            //string eAddr = @"No. 45, Aly. 4, Ln. 23, Da’an St., Yuanlin City, Changhua County, Taiwan (R.O.C.)".Replace(" ", "");

            //if (Free.Replace(" ", "") != add.Replace(" ", ""))
            //{
            //    Console.WriteLine("1");
            //}
            //else
            //{
            //    Console.WriteLine("2");
            //}
            #endregion

            #region AMS function test
            DataTable dt = new DataTable("Order");
            DataColumn dc = dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(String));

            dt.Rows.Add(1, "pramod");
            dt.Rows.Add(2, "ravi");
            dt.Rows.Add(3, "deepak");
            dt.Rows.Add(4, "kiran");
            dt.Rows.Add(5, "madhu");

            DataTable dt2 = new DataTable("Order");
            DataColumn dc2 = dt2.Columns.Add("ID", typeof(int));
            dt2.Columns.Add("Name", typeof(String));
            dt2.Columns.Add("Type", typeof(String));

            dt2.Rows.Add(6, "ashu", "Gen");
            dt2.Rows.Add(7, "rudra", "Gen");
            dt2.Rows.Add(8, "kavita", "Gen");
            dt2.Rows.Add(9, "suman", "Gen");
            dt2.Rows.Add(10, "lakshman", "Gen");

            DataTable dt3 = new DataTable();
            dt3.Merge(dt);
            dt3.Merge(dt2);

            string bbbbbb = JsonConvert.SerializeObject(dt);
            DataTable dt5 = JsonConvert.DeserializeObject<DataTable>(bbbbbb);
            string bbbbbbb = JsonConvert.SerializeObject(dt);

            //foreach (DataRow dr in dt3.Rows)
            //{
            //    foreach (var item in dr.ItemArray)
            //    {
            //        Console.Write((item.ToString() + "\t").PadLeft(20));
            //    }
            //    Console.WriteLine("\n");
            //}

            //string Kind = "ELI";
            //string NationName = "";

            //xmldoc.Load(new StringReader("<Address><Nation>TW</Nation><County>undefined</County><District>undefined</District><Free>No. 11, Minghu 5th St., Baoshan Township, Hsinchu County, Taiwan (R.O.C.)</Free><NationName>TAIWAN   台灣</NationName></Address>"));
            //string Add = "";
            //if (Kind != "CCOM" && xmldoc.GetElementsByTagName("Nation").Count > 0)
            //    Nation = xmldoc.GetElementsByTagName("Nation")[0].InnerText;
            //if (Kind != "CCOM" && xmldoc.GetElementsByTagName("NationName").Count > 0)
            //    NationName = xmldoc.GetElementsByTagName("NationName")[0].InnerText;

            //if (xmldoc.GetElementsByTagName("County").Count > 0 && xmldoc.GetElementsByTagName("County")[0].InnerText != "undefined")
            //    Add += xmldoc.GetElementsByTagName("County")[0].InnerText;
            //if (xmldoc.GetElementsByTagName("District").Count > 0 && xmldoc.GetElementsByTagName("District")[0].InnerText != "undefined")
            //    Add += xmldoc.GetElementsByTagName("District")[0].InnerText;

            //if (xmldoc.GetElementsByTagName("Neighbor").Count > 0 && !string.IsNullOrEmpty(xmldoc.GetElementsByTagName("Neighbor")[0].InnerText))
            //    Add += xmldoc.GetElementsByTagName("Neighbor")[0].InnerText + "里";
            //if (xmldoc.GetElementsByTagName("Neighbor2").Count > 0 && !string.IsNullOrEmpty(xmldoc.GetElementsByTagName("Neighbor2")[0].InnerText))
            //    Add += xmldoc.GetElementsByTagName("Neighbor2")[0].InnerText + "鄰";
            //if (xmldoc.GetElementsByTagName("Road").Count > 0 && !string.IsNullOrEmpty(xmldoc.GetElementsByTagName("Road")[0].InnerText))
            //    Add += xmldoc.GetElementsByTagName("Road")[0].InnerText;
            //if (xmldoc.GetElementsByTagName("Lane").Count > 0 && !string.IsNullOrEmpty(xmldoc.GetElementsByTagName("Lane")[0].InnerText))
            //    Add += xmldoc.GetElementsByTagName("Lane")[0].InnerText + "巷";
            //if (xmldoc.GetElementsByTagName("Alley").Count > 0 && !string.IsNullOrEmpty(xmldoc.GetElementsByTagName("Alley")[0].InnerText))
            //    Add += xmldoc.GetElementsByTagName("Alley")[0].InnerText + "弄";
            //if (xmldoc.GetElementsByTagName("No").Count > 0 && !string.IsNullOrEmpty(xmldoc.GetElementsByTagName("No")[0].InnerText))
            //    Add += xmldoc.GetElementsByTagName("No")[0].InnerText + "號";
            //if (xmldoc.GetElementsByTagName("No2").Count > 0 && !string.IsNullOrEmpty(xmldoc.GetElementsByTagName("No2")[0].InnerText))
            //    Add += "之" + xmldoc.GetElementsByTagName("No2")[0].InnerText;
            //if (xmldoc.GetElementsByTagName("Floor").Count > 0 && !string.IsNullOrEmpty(xmldoc.GetElementsByTagName("Floor")[0].InnerText))
            //    Add += xmldoc.GetElementsByTagName("Floor")[0].InnerText + "樓";
            //if (xmldoc.GetElementsByTagName("Floor2").Count > 0 && !string.IsNullOrEmpty(xmldoc.GetElementsByTagName("Floor2")[0].InnerText))
            //    Add += "之" + xmldoc.GetElementsByTagName("Floor2")[0].InnerText;
            //if (xmldoc.GetElementsByTagName("Room").Count > 0 && !string.IsNullOrEmpty(xmldoc.GetElementsByTagName("Room")[0].InnerText))
            //    Add += xmldoc.GetElementsByTagName("Room")[0].InnerText + "室";

            //if (xmldoc.GetElementsByTagName("Free").Count > 0 && !string.IsNullOrEmpty(xmldoc.GetElementsByTagName("Free")[0].InnerText))
            //    Add += xmldoc.GetElementsByTagName("Free")[0].InnerText;

            //Console.WriteLine(Add);

            //List<string> IsChange = new List<string>();

            //IsChange.Add("信用卡月結單");
            //IsChange.Add("信用卡日結單");

            //string aaaaaa = string.Join(",", IsChange.ToArray());

            //string aaaaaaaa = ";2;3";
            //Console.WriteLine("llll"+aaaaaaaa.Split(';').Length);

            //Console.WriteLine(isNumber("0").ToString());


            //int WMP_EXPIRY_DATE_V = 0;
            //int.TryParse("", out WMP_EXPIRY_DATE_V);
            //Console.WriteLine(int.Parse(DateTime.Today.ToString("yyyyMMdd")));
            //Console.WriteLine(WMP_EXPIRY_DATE_V);

            //Console.WriteLine(Convert.ToString(DBNull.Value));
            //Console.WriteLine((Convert.ToString(DBNull.Value)=="").ToString());
            #endregion

            #region RSA Signature
            //string jsonString = JObject.Parse(gg).ToString(Newtonsoft.Json.Formatting.None);

            //using (TextReader privateKeyTextReader = new StringReader(File.ReadAllText(@"D:\Jonathan\SSOServerKey\rsaPri.pem")))
            //{
            //    using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            //    {
            //        var pemReader = new PemReader(privateKeyTextReader);
            //        AsymmetricCipherKeyPair privateKey = (AsymmetricCipherKeyPair)pemReader.ReadObject();

            //        // 使用SHA256算法对数据进行哈希
            //        var sha256Digest = new Sha256Digest();
            //        var hash = Encoding.UTF8.GetBytes(jsonString);
            //        sha256Digest.BlockUpdate(hash, 0, hash.Length);
            //        var hashValue = new byte[sha256Digest.GetDigestSize()];
            //        sha256Digest.DoFinal(hashValue, 0);

            //        // 创建签名器
            //        var signer = SignerUtilities.GetSigner("SHA256withRSA");
            //        signer.Init(true, privateKey.Private);
            //        signer.BlockUpdate(hashValue, 0, hashValue.Length);

            //        // 生成数字签名
            //        var signature = signer.GenerateSignature();

            //        TextReader publicKeyTextReader = new StringReader(File.ReadAllText(@"D:\Jonathan\SSOServerKey\rsaPub.pem"));
            //        string publicKey = publicKeyTextReader.ReadToEnd();
            //        publicKey = publicKey.Replace("\n", "").Replace("-----BEGIN PUBLIC KEY-----", "").Replace("-----END PUBLIC KEY-----", "");
            //    }
            //}
            #endregion

            #region XML TEST
            //try
            //{
            //    throw new Exception("ErrorCount : 10");
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}

            //SetConnection("CASA", "CONNGGS");
            //string UnitAddressCountry = "";

            //string xmlLoadStr = "<Address><Nation>TW</Nation><County>新北市</County><District>林口區</District><Neighbor></Neighbor><Neighbor2></Neighbor2><Road>井泉街</Road><Lane></Lane><Alley></Alley><No>1</No><No2></No2><Floor></Floor><Floor2></Floor2><Room></Room><NationName>Taiwan 台灣</NationName></Address>";
            //if (!string.IsNullOrEmpty(xmlLoadStr))
            //{
            //    XmlDocument xmldoc = new XmlDocument();
            //    xmldoc.Load(new StringReader(xmlLoadStr));
            //    if (xmldoc.GetElementsByTagName("Nation").Count != 0)
            //        UnitAddressCountry = xmldoc.GetElementsByTagName("Nation")[0].InnerText;
            //}
            //DateTime dtt = Convert.ToDateTime("0");
            #endregion

            #region email test
            //設定發送郵件主機
            SmtpClient sc = new SmtpClient();
            sc.Host = "mail.asiavista.com.tw";

            System.Net.NetworkCredential myMailCredential = new System.Net.NetworkCredential();
            myMailCredential.UserName = "jonathan_huang@asiavista.com.tw";
            myMailCredential.Password = "sandy520";

            sc.UseDefaultCredentials = false;
            sc.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            sc.Credentials = myMailCredential;
            sc.Port = 25;

            string to = "jonathan_huang@asiavista.com.tw";
            string from = "jonathan_huang@asiavista.com.tw";
            var message = new MailMessage(from, to);
            message.Subject = "饋贈招待及商業午晚餐申報及申請(Request for Gift, Entertainment & Business lunch)[取件]";
            message.Body = "<head> <meta http-equiv=\"Content - Type\" content=\"text / html; charset = big5\"> <title></title> <style type=\"text / css\"> .ms-formlabel { PADDING-RIGHT: 8px; PADDING-Left: 5px; BORDER-TOP: #d8d8d8 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 12px; PADDING-BOTTOM: 6px; COLOR: #525252; PADDING-TOP: 3px; FONT-FAMILY: Arial, Helvetica, sans-serif; TEXT-ALIGN: left; } .ms-formlabel-center { height: 40px; PADDING-RIGHT: 8px; BORDER-TOP: #d8d8d8 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 12px; PADDING-BOTTOM: 6px; COLOR: #525252; PADDING-TOP: 3px; FONT-FAMILY: Arial, Helvetica, sans-serif; TEXT-ALIGN: center; } .ms-formbody { PADDING-RIGHT: 6px; BORDER-TOP: #d8d8d8 1px solid; COLOR: #666666; PADDING-LEFT: 6px; FONT-SIZE: 12px; BACKGROUND: #f7f6f3; PADDING-BOTTOM: 4px; PADDING-TOP: 3px; FONT-FAMILY: Arial, Helvetica, sans-serif; } .style1 { font-family: \"Courier New\"; } </style></head><body> <br /> <table align=\"center\" width=\"80 % \" id=\"table2\" cellspacing=\"0\" cellpadding=\"1\"> <tr> <td bgcolor=\"#FF6600\" height=\"22\"> <p align=\"center\"><font face=\"Arial\" color=\"#ffffff\" size=\"2\"><b>{FormName}</b></font> </td> </tr> <tr> <td height=\"6\"></td> </tr> </table> <div align=\"center\"> <table width=\"80%\" id=\"table1\" style=\"border-collapse: collapse\" border=\"0\" bordercolor=\"#000000\"> <tr> <td class=\"ms-formlabel\" style=\"word-wrap:break-word; display: inline-block;\" width=\"30%\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">表單編號(FormNo)</font> </td> <td class=\"ms-formbody\" width=\"70%\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">{CM01_FormNo}</font> </td> </tr> <tr> <td class=\"ms-formlabel\" style=\"word-wrap:break-word; display: inline-block;\" width=\"30%\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">登錄日期(Register Date)</font> </td> <td class=\"ms-formbody\" width=\"70%\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">{CM01_CreateDate}</font> </td> </tr> <tr> <td class=\"ms-formlabel\" style=\"word-wrap:break-word;display: inline-block;\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">登錄人(Registrant)</font> </td> <td class=\"ms-formbody\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">{CM01_CreateID}</font> </td> </tr> <tr> <td class=\"ms-formlabel\" style=\"word-wrap:break-word;display: inline-block;\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">接受(提供)日期(Date Received/Offered)</font> </td> <td class=\"ms-formbody\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">{CM01_AcceptDate}</font> </td> </tr> <tr> <td class=\"ms-formlabel\" style=\"word-wrap:break-word;display: inline-block;\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">申請部門(Applicant's Department)</font> </td> <td class=\"ms-formbody\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">{CM01_DeptID}</font> </td> </tr> <tr> <td class=\"ms-formlabel\" style=\"word-wrap:break-word;display: inline-block;\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">申請人(Applicant's name)</font> </td> <td class=\"ms-formbody\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">{CM01_Requester}</font> </td> </tr> <tr> <td class=\"ms-formlabel\" style=\"word-wrap:break-word;display: inline-block;\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">對象類別(Object Category)</font> </td> <td class=\"ms-formbody\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">{CM01_Type_IsGov}</font> </td> </tr> <tr> <td class=\"ms-formlabel\" style=\"word-wrap:break-word;display: inline-block;\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">行為類別(Category)</font> </td> <td class=\"ms-formbody\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">{CM01_Type_Target}</font> </td> </tr> <tr> <td class=\"ms-formlabel\" style=\"word-wrap:break-word;display: inline-block;\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">接受或提供類別(Receive or Offer)</font> </td> <td class=\"ms-formbody\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">{CM01_Type_Accept}</font> </td> </tr> <tr> <td class=\"ms-formlabel\" style=\"word-wrap:break-word;display: inline-block;\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">總金額(Total Amount)</font> </td> <td class=\"ms-formbody\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">{CM01_TotalAmount}</font> </td> </tr> <tr> <td class=\"ms-formlabel\" style=\"word-wrap:break-word;display: inline-block;\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">人數(Numbers of Total Participants)</font> </td> <td class=\"ms-formbody\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">{CM01_Num}</font> </td> </tr> <tr> <td class=\"ms-formlabel\" style=\"word-wrap:break-word;display: inline-block;\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">人均金額(Averaged amount per person)</font> </td> <td class=\"ms-formbody\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">{CM01_PerAmount}</font> </td> </tr> <tr> <td class=\"ms-formlabel\" style=\"word-wrap:break-word;display: inline-block;\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">饋贈/招待/商業午晚餐名稱(Description ofGift/Entertainment/BusinessLunch/Dinner)</font> </td> <td class=\"ms-formbody\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">{CM01_MealName}</font> </td> </tr> <tr> <td class=\"ms-formlabel\" style=\"word-wrap:break-word;display: inline-block;\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">本公司參與人員(Participants from NomuraSITE)</font> </td> <td class=\"ms-formbody\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">{SummaryReportDataSource_UserID}</font> </td> </tr> <tr> <td class=\"ms-formlabel\" style=\"word-wrap:break-word;display: inline-block;\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">接受(提供)對象所屬單位名稱(Receive/Offerfrom/to the Entity's Name)</font> </td> <td class=\"ms-formbody\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">{CM01_AcceptSection}</font> </td> </tr> <tr> <td class=\"ms-formlabel\" style=\"word-wrap:break-word;display: inline-block;\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">接受(提供)對象所屬單位統編(Receive/Offerfrom/to the Entity's Unified BusinessNo.)</font> </td> <td class=\"ms-formbody\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">{CM01_AcceptUniNo}</font> </td> </tr> <tr> <td class=\"ms-formlabel\" style=\"word-wrap:break-word;display: inline-block;\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">接受(提供)對象名字/組織名稱(Receive/Offerfrom/to name/organization)</font> </td> <td class=\"ms-formbody\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">{SummaryReportDataSource_CustName}</font> </td> </tr> <tr> <td class=\"ms-formlabel\" style=\"word-wrap:break-word;display: inline-block;\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">說明(Description)</font> </td> <td class=\"ms-formbody\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">{CM01_Description}</font> </td> </tr> <tr> <td class=\"ms-formlabel\" style=\"word-wrap:break-word;display: inline-block;\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">接受或提供饋贈/招待/商業午晚餐之關係(Pleasedescribe the relationship why tooffer or receive ofGift/Entertainment/BusinessLunch/Dinner to/from external party?)</font> </td> <td class=\"ms-formbody\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">{CM01_AcceptRelationship}</font> </td> </tr> <tr> <td class=\"ms-formlabel\" style=\"word-wrap:break-word;display: inline-block;\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">接受或提供該饋贈/招待/商業午晚餐是否產生或可能發生利益衝突或賄賂之情形?(Pleasedescribe if conflict of interest orBribery might be suffered whileGift/Entertainment/BusinessLunch/Dinner is offered or received?)</font> </td> <td class=\"ms-formbody\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">{CM01_IsBribery}</font> </td> </tr> <tr> <td class=\"ms-formlabel\" style=\"word-wrap:break-word;display: inline-block;\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">接受或提供該饋贈/招待/商業午晚餐是否意味員工本人或公司需提供任何承諾或義務?(Pleasedescribe if any commitment orobligation might be imposed when theCompany or employee offers or receivesGift/Entertainment/Business Lunch/Dinnerto/from external parties?)</font> </td> <td class=\"ms-formbody\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">{CM01_IsPromise}</font> </td> </tr> <tr> <td class=\"ms-formlabel\" style=\"word-wrap:break-word;display: inline-block;\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">Special Approve編號(Tracking number grantedfor Special Approval)</font> </td> <td class=\"ms-formbody\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\">{CM01_SpecialApproveUniNo}</font> </td> </tr> <tr> <td class=\"ms-formlabel\" style=\"word-wrap:break-word;display: inline-block;\"></td> <td class=\"ms-formbody\"> <font size=\"2\" face=\"Courier New\" color=\"#666666\"><a href=\"{FormLink}\">表單連結</a></font> </td> </tr> <tr> <td class=\"ms-formlabel\" colspan=\"2\"> </td> </tr> </table> </div> <hr></body></html>";
            message.IsBodyHtml = true;
            //sc.Send(message);
            #endregion

            #region Excel下拉式
            //string filePath = "";
            //IWorkbook workbook = null;
            //ISheet sheet = null;

            //using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            //{
            //    workbook = new XSSFWorkbook(fs);
            //    fs.Close();
            //}
            //if(workbook != null)
            //{
            //    sheet = workbook.GetSheetAt(0);
            //}

            //IDataValidationHelper dataValidationHelper = new XSSFDataValidationHelper((XSSFSheet)sheet);
            //CellRangeAddressList cellRangeAddressList = new CellRangeAddressList(0, 0, 0, 0);
            //IDataValidationConstraint dataValidationConstraint = dataValidationHelper.CreateExplicitListConstraint(new string[] { "1", "2", "3" });
            //IDataValidation dataValidation = dataValidationHelper.CreateValidation(dataValidationConstraint, cellRangeAddressList);

            //dataValidation.ShowErrorBox = false;
            //sheet.AddValidationData(dataValidation);

            //FileStream fileStream = File.Create(filePath);
            //workbook.Write(fileStream);
            //fileStream.Close();
            #endregion

            #region itextsharp 4.1.6
            //    string html = "<html> <header> <title></title> <meta http-equiv=\"Content-Type\" content=\"text/html; charset=big5\"/> </header> <body> <br /> <br /> <table border=\"0\" width=\"100%\" height=\"150\"> <tr> <td></td> <td></td> </tr> <tr> <td></td> <td></td> </tr> <tr> <td colspan=\"2\" align=\"center\" bgcolor=\"#0073AB\"> <font color=\"#FFFFFF\" size=\"2\">e元富始申請書</font> </td> </tr> <tr> <td colspan=\"2\"> <table width=\"100%\" style=\"height:150px\"> <tr> <td colspan=\"2\"> <font size=\"1\">申請人茲聲明已於合理時間內審慎閱讀並收受貴行各項產品約定書所載各項條款，並同意其內容，惠請貴行辦理申請人向貴行申請勾填之業務項目。(未勾選之項目視為客戶不申請)123123</font> </td> </tr> <tr> <td> <font size=\"1\">□ 貴行開戶總約定書</font> </td> <td> <font size=\"1\">(版本：SCB <u>1111111111</u>)</font> </td> </tr> <tr> <td> <font size=\"1\">□ 消費金融服務收費標準 kirk from E元富使 3</font> </td> <td> <font size=\"1\">(版本：SCB <u>1111111111</u>)</font> </td> </tr> <tr> <td> <font size=\"1\">□ VISA金融卡扣款約定事項(VISA金融卡權益手冊) 拜託不要4</font> </td> <td> <font size=\"1\">(版本：SCB <u>1111111111</u>)</font> </td> </tr> <tr> <td> <font size=\"1\">□ 個人網路銀行使用說明小卡拜託不要亂更新了4</font> </td> <td> <font size=\"1\">(版本：SCB <u>1111111111</u>)</font> </td> </tr> <tr> <tr> <td colspan=\"2\"> <font size=\"1\">申請人茲聲明本次不同時申請開立信託帳戶，並同意開戶總約定書內關於信託帳戶約定事項之各項條款不適用於本次申請人。321323</font> </td> <td></td> </tr> </table> </td> </tr> </table> <table border=\"0\" width=\"100%\"> <tr> <td colspan=\"4\" color=\"#0073AB\" size=\"1\"> _________________________________________________________________________________________________________________________________ </td> </tr> <tr> <td> <font size=\"2\" color=\"#28AB47\">□e元富始組合</font> </td> <td> <font size=\"2\" color=\"#28AB47\">其他精選產品</font> </td> <td> </td> <td> </td> </tr> <tr> <td> <table border=\"0\" width=\"100%\"> <td> <font size=\"1\">1.渣打存款帳戶</font> </td> </tr> <tr> <td> <font size=\"1\">2.網路銀行</font> </td> </tr> <tr> <td> <font size=\"1\">3.□VISA金融卡或□信用卡</font> </td> </tr> <tr> <td> <font size=\"1\">4.電話語音</font> </td> </tr> <tr> <td> <font size=\"1\">5.電子綜合月結單</font> </td> </tr> </table> </td> <td> <table border=\"0\" width=\"100%\"> <tr> <td> <font size=\"1\">□外幣存款帳戶</font> </td> </tr> <tr> <td> <font size=\"1\">□信託相關產品</font><font size=\"1\" color=\"red\">(需另填寫申請書)</font> </td> </tr> <tr> <td> <font size=\"1\">□信用貸款</font> </td> </tr> <tr> <td> <font size=\"1\">□公用事業費用代轉繳服務</font> </td> </tr> </table> </td> <td width=\"25%\"> 或 </td> <td> <font size=\"2\" color=\"#28AB47\">□渣打存款帳戶</font> </td> </tr> </table> <table border=\"0\" width=\"100%\"> <tr> <td width=\"4%\" bgcolor=\"#008AC2\" height=\"27\" align=\"center\"><font color=\"#FFFFFF\" size=\"2\">◎</font></td> <td colspan=\"2\" bgcolor=\"#70BC84\" height=\"27\"><font color=\"#FFFFFF\" size=\"2\">&nbsp;&nbsp;&nbsp;&nbsp;客戶基本資料</font><font color=\"#FFFFFF\" size=\"1\">(提醒您：自101 年8 月2 日起，若您未填寫此次申請書所列之個人資料相關欄位，本行將會直接援引您最近一次與本行業務往來所提供之個人資料作為本次申請使用。)</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">中文姓名</font></td> <td><font size=\"2\"> 高富帥 </font> </td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">中文別名</font></td> <td><font size=\"2\">無 N/A</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">性別</font></td> <td><font size=\"2\">女</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">身分證字號/統一證號</font></td> <td><font size=\"2\">K221138305</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">第一證件類別</font></td> <td><font size=\"2\">身分證</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">第一證件到期日</font></td> <td><font size=\"2\">9999/12/31</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">英文姓</font></td> <td><font size=\"2\">KAO,</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">英文名</font></td> <td><font size=\"2\">FU SHUO</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">英文中間名</font></td> <td><font size=\"2\">無 N/A</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">證件英文名</font></td> <td><font size=\"2\">無 N/A</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">英文別名</font></td> <td><font size=\"2\">無 N/A</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">護照號碼</font></td> <td><font size=\"2\">無 N/A</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">出生年月日</font></td> <td><font size=\"2\">民國95年01月01日</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">國籍</font></td> <td><font size=\"2\">Taiwan 台灣</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">電子郵件</font></td> <td><font size=\"2\">aaa@gmail.com</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">服務公司</font></td> <td><font size=\"2\">?</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">職稱</font></td> <td><font size=\"2\">test</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">職級</font></td> <td><font size=\"2\">Salaried (Controller/Owner/Director)</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">開戶目的?</font></td> <td><font size=\"2\">一般/個人支出</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">EB客戶之公司統編</font></td> <td><font size=\"2\">無 N/A</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">初始存入金額</font></td> <td width=\"34%\" align=\"left\" >新台幣【1】萬元</br> <font size=\"1\" color=\"#0066FF\">(預計開戶新入帳金額)</font> </td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">抵押貸款額度</font></td> <td width=\"34%\" align=\"left\" >新台幣【1】萬元</br> <font size=\"1\" color=\"#0066FF\">(於本行已核准或申請中之抵押貸款額度)</font> </td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">年收入</font></td> <td width=\"34%\" align=\"left\" >新台幣【72】萬元</br> <font size=\"1\" color=\"#0066FF\"></font> </td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">每月預計存款或匯入總金額</font></td> <td width=\"34%\" align=\"left\" >新台幣【1】萬元</br> <font size=\"1\" color=\"#0066FF\"></font> </td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">每月預計存入或匯入次數</font></td> <td><font size=\"2\">1</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">每月預計提款或匯款總金額</font></td> <td width=\"34%\" align=\"left\" >新台幣【1】萬元</br> <font size=\"1\" color=\"#0066FF\"></font> </td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">每月預計提款或匯款次數</font></td> <td><font size=\"2\">1</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">開立非居民帳戶(為非持中華民國身分證或居留證開戶者)?</font></td> <td><font size=\"2\">否</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">其他國籍1</font></td> <td><font size=\"2\">無 N/A</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">其他國籍稅籍編號1</font></td> <td><font size=\"2\">無 N/A</font></td> </tr> <tr> <td width=\"4%\" bgcolor=\"#008AC2\" height=\"27\" align=\"center\"><font color=\"#FFFFFF\" size=\"2\">◎</font></td> <td colspan=\"2\" bgcolor=\"#70BC84\" height=\"27\"><font color=\"#FFFFFF\" size=\"2\">&nbsp;&nbsp;&nbsp;&nbsp;客戶電話資料</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">電話(行動)</font></td> <td><font size=\"2\">0987654321</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">電話(住宅)</font></td> <td><font size=\"2\">無 N/A</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">電話(公司)</font></td> <td><font size=\"2\">無 N/A</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">電話(戶籍)</font></td> <td><font size=\"2\">無 N/A</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">電話(傳真)</font></td> <td><font size=\"2\">無 N/A</font></td> </tr> <tr> <td width=\"4%\" bgcolor=\"#008AC2\" height=\"27\" align=\"center\"><font color=\"#FFFFFF\" size=\"2\">◎</font></td> <td colspan=\"2\" bgcolor=\"#70BC84\" height=\"27\"><font color=\"#FFFFFF\" size=\"2\">&nbsp;&nbsp;&nbsp;&nbsp;客戶地址資料</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">戶籍地址</font></td> <td><font size=\"2\"> Taiwan 台灣 新北市中和區中正路777號 </font> </td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">通訊地址</font></td> <td><font size=\"2\"> TAIWAN 台灣 新竹市東區光復路一段５２５巷?４９號 </font> </td> </tr> <tr> <td width=\"4%\" bgcolor=\"#008AC2\" height=\"27\" align=\"center\"><font color=\"#FFFFFF\" size=\"2\">◎</font></td> <td colspan=\"2\" bgcolor=\"#70BC84\" height=\"27\"><font color=\"#FFFFFF\" size=\"2\">&nbsp;&nbsp;&nbsp;&nbsp;法定代理人</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">法定代理人姓名</font></td> <td><font size=\"2\"> 無 N/A </font> </td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">法定代理人身份證字號</font></td> <td><font size=\"2\">無 N/A</font></td> </tr> <tr> <td width=\"4%\" bgcolor=\"#008AC2\" height=\"27\" align=\"center\"><font color=\"#FFFFFF\" size=\"2\">◎</font></td> <td colspan=\"2\" bgcolor=\"#70BC84\" height=\"27\"><font color=\"#FFFFFF\" size=\"2\">&nbsp;&nbsp;&nbsp;&nbsp;帳戶種類</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">台幣有摺/無摺</font></td> <td><font size=\"2\">有摺</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">台幣存款種類</font></td> <td><font size=\"2\">活期存款</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">台幣活存帳號</font></td> <td> <table> <tr> <td style=\"padding-top:10\"> <font size=\"2\"><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /></font> </td> <td> <font size=\"1\" color=\"#0066FF\">(帳號由行員填寫)</font> </td> </tr> </table> </td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">外幣帳戶</font></td> <td><font size=\"2\">不申請</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">外幣有摺/無摺</font></td> <td><font size=\"2\">無 N/A</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">外幣存款種類</font></td> <td><font size=\"2\">無 N/A</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">外幣活存帳號</font></td> <td> <table> <tr> <td style=\"padding-top:10\"> <font size=\"2\"><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /></font> </td> <td> <font size=\"1\" color=\"#0066FF\">(帳號由行員填寫)</font> </td> </tr> </table> </td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">幣別</font></td> <td><font size=\"2\">無 N/A</font></td> </tr> <tr> <td width=\"4%\" bgcolor=\"#008AC2\" height=\"27\" align=\"center\"><font color=\"#FFFFFF\" size=\"2\">◎</font></td> <td colspan=\"2\" bgcolor=\"#70BC84\" height=\"27\"><font color=\"#FFFFFF\" size=\"2\">&nbsp;&nbsp;&nbsp;&nbsp;服務選擇</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">網路銀行</font></td> <td> <font size=\"2\">申請<font color=\"#FF0000\">(申請台幣非約定轉帳功能)</font></font></br> <font size=\"1\" color=\"#0066FF\"></font> </td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">包括外匯交易及基金交易服務</font></td> <td><font size=\"2\">同意</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">網路銀行驗印帳號</font></td> <td> <table width=\"100%\"> <tr> <td align=\"left\"> <font size=\"2\"><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /></font></br> </td> <td style=\"padding-left:70\" > <img alt=\"\" width=\"120\" height=\"60\" src=\"http://localhost:57263/Images/stamp.png\" /> </td> </tr> </table> </td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">電話語音</font></td> <td> <font size=\"2\">不申請<font color=\"#FF0000\">(不申請台幣非約定轉帳功能)</font></font></br> </td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">電話語音驗印帳號</font></td> <td> <table> <tr> <td> <font size=\"2\"><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /><img alt=\"\" width=\"12\" height=\"12\" src=\"http://localhost:57263/Images/number/space.jpg\" /></font><br/> <font size=\"1\"><img alt=\"\" width=\"6\" height=\"6\" src=\"http://localhost:57263/Images/number/space.jpg\" />同網路銀行</font> </td> <td style=\"padding-left:70\"> <img alt=\"\" width=\"120\" height=\"60\" src=\"http://localhost:57263/Images/stamp.png\" /> </td> </tr> </table> </td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">綜合月結單</font></td> <td><font size=\"2\">Email寄送</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">推薦人是否已瞭解此推薦專案</font></td> <td><font size=\"2\">推薦人不符合活動專案資格</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">是否升等為優逸理財</font></td> <td><font size=\"2\">無 N/A</font></td> </tr> <tr> <td width=\"4%\" bgcolor=\"#008AC2\" height=\"27\" align=\"center\"><font color=\"#FFFFFF\" size=\"2\">◎</font></td> <td colspan=\"2\" bgcolor=\"#70BC84\" height=\"27\"><font color=\"#FFFFFF\" size=\"2\">&nbsp;&nbsp;&nbsp;&nbsp;約定轉帳設定</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">台幣約定轉帳</font></td> <td> <table width=\"100%\" border=\"0\"> <tr> <td><font size=\"1\"> 無 N / A </font> </td> </tr> </table> </td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">外幣約定轉帳</font></td> <td> <table width=\"100%\" border=\"0\"> <tr> <td><font size=\"1\"> 無 N / A </font> </td> </tr> </table> </td> </tr> <tr> <td width=\"4%\" bgcolor=\"#008AC2\" height=\"27\" align=\"center\"><font color=\"#FFFFFF\" size=\"2\">◎</font></td> <td colspan=\"2\" bgcolor=\"#70BC84\" height=\"27\"><font color=\"#FFFFFF\" size=\"2\">&nbsp;&nbsp;&nbsp;&nbsp;VISA金融卡</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">申請狀態</font></td> <td><font size=\"2\">申請</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">卡片及密碼單領取方式</font></td> <td><font size=\"2\">臨櫃親領</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#FF0000\" size=\"2\">消費扣款功能</font></td> <td> <font size=\"2\" color=\"#FF0000\" >不開啟</font></br> </td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#FF0000\" size=\"2\">非約定轉帳功能</font></td> <td> <font size=\"2\" color=\"#FF0000\" >不開啟</font></br> </td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td width=\"34%\" align=\"left\" ><font color=\"#FF0000\" size=\"2\">跨國提款功能</font></td> <td> <font size=\"2\" color=\"#FF0000\" >不開啟</font></br> </td> </tr> <tr> <td width=\"4%\" bgcolor=\"#008AC2\" height=\"27\" align=\"center\"><font color=\"#FFFFFF\" size=\"2\">◎</font></td> <td colspan=\"2\" bgcolor=\"#70BC84\" height=\"27\"><font color=\"#FFFFFF\" size=\"2\">&nbsp;&nbsp;&nbsp;&nbsp;</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"><font size=\"1\">一</font></td> <td colspan=\"2\"><font size=\"2\">申請人茲此確認以下所提供資訊真實、完整且正確(僅為存款帳戶申請適用)。</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td colspan=\"2\" > <table width=\"100%\"> <tr> <td width=\"45%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">申請人聲明 申請人為美國公民</font></td> <td> <table width=\"100%\"> <tr> <td width=\"15%\"> <img width=\"8px\" height=\"8px\" src=\"http://localhost:57263/Images/CheckBoxFalse.png\">&nbsp;<font size=\"2\">是&nbsp;&nbsp;</font> </td> <td align=\"left\"> <img width=\"8px\" height=\"8px\" src=\"http://localhost:57263/Images/CheckBoxTrue.png\">&nbsp;<font size=\"2\">否</font> </td> </tr> </table> </td> </tr> </table> </td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td colspan=\"2\" > <table width=\"100%\"> <tr> <td width=\"45%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">申請人聲明 申請人持有美國綠卡</font></td> <td> <table width=\"100%\"> <tr> <td width=\"15%\"> <img width=\"8px\" height=\"8px\" src=\"http://localhost:57263/Images/CheckBoxFalse.png\">&nbsp;<font size=\"2\">是&nbsp;&nbsp;</font> </td> <td align=\"left\"> <img width=\"8px\" height=\"8px\" src=\"http://localhost:57263/Images/CheckBoxTrue.png\">&nbsp;<font size=\"2\">否</font> </td> </tr> </table> </td> </tr> </table> </td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td colspan=\"2\" > <table width=\"100%\"> <tr> <td width=\"45%\" align=\"left\" ><font color=\"#28AB47\" size=\"2\">申請人聲明 申請人為美國居民</font></td> <td> <table width=\"100%\"> <tr> <td width=\"15%\"> <img width=\"8px\" height=\"8px\" src=\"http://localhost:57263/Images/CheckBoxFalse.png\">&nbsp;<font size=\"2\">是&nbsp;&nbsp;</font> </td> <td align=\"left\"> <img width=\"8px\" height=\"8px\" src=\"http://localhost:57263/Images/CheckBoxTrue.png\">&nbsp;<font size=\"2\">否</font> </td> </tr> </table> </td> </tr> </table> </td> </tr> <tr> <td width=\"4%\" height=\"30\"></td> <td colspan=\"2\" ><img width=\"8px\" height=\"8px\" src=\"http://localhost:57263/Images/CheckBoxTrue.png\">&nbsp;<font size=\"2\"> 於本地法規允許範圍內，申請人同意 貴行得提供申請人之個人資訊(包括但不限於姓名、出生日期、出生地、國籍、地址、財務情況等資料)予 貴 行之關係企業、 貴行之母行或其關係企業(包含其分行)及 貴行或 貴行國內與海外之關係企業委託處理業務之第三人，其或貴行並得進一步提供 予國內外之稅務機關，以履行申請人於任何司法管轄區下稅務責任。申請人並同意 貴行或其關係企業、 貴行之母行或其關係企業(包含其分行) 得依國內外主管機關或稅務機關之要求，依據所適用之法規、指令，自申請人帳戶中代為扣繳應扣繳之金額。<font color=\"#FF0000\">申請人瞭解，若不同意將個人資料於 上述目的及範圍之利用，及同意上述代扣繳事項， 貴行基於國內外稅法相關規範，將無法核准申請人開立帳戶。</font></font> </td> </tr> <tr> <td width=\"4%\" height=\"30\"><font size=\"1\">二</font></td> <td colspan=\"2\"><font size=\"2\">個人資料之蒐集、處理、利用特別約定條款：</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"><font size=\"1\">1.</font></td> <td colspan=\"2\"><font size=\"2\">本人確認已收到銀行之「個人資料蒐集、處理、利用告知事項」(以下稱「個資告知事項」)</font></td> </tr> <tr> <td width=\"4%\" height=\"30\"><font size=\"1\">2.</font></td> <td colspan=\"2\"><font size=\"2\">申請人確認提供之資料均正確無誤，且授權銀行得於銀行所定特定目的範圍內隨時向有關各方(包括但不限於財團法人金融聯合徵信中心、勞保局)查證、蒐集申請人之相關個人資料，並得就該個人資料為處理、利用及國際傳輸。申請人資料有所變更時，應儘速通知 貴行。</font></td> </tr> <tr> <td width=\"4%\" height=\"30\" valign=\"top\"><font size=\"1\">3.</font></td> <td colspan=\"2\"> <font size=\"2\" color=\"#FF0000\">存款帳戶共同行銷條款：申請人 <img width=\"8px\" height=\"8px\" src=\"http://localhost:57263/Images/CheckBoxFalse_Red.png\">同意 <img width=\"8px\" height=\"8px\" src=\"http://localhost:57263/Images/CheckBoxTrue_Red.png\">不同意</font> </td> </tr> <tr> <td width=\"4%\" height=\"30\">&nbsp;</td> <td width=\"34%\" align=\"left\">&nbsp;</td> <td>&nbsp;</td> </tr> <tr> <tr> <td width=\"4%\" height=\"30\">&nbsp;</td> <td width=\"34%\" align=\"left\">&nbsp;</td> <td>&nbsp;</td> </tr> <tr> <tr> <td width=\"4%\" height=\"30\">&nbsp;</td> <td width=\"34%\" align=\"left\">&nbsp;</td> <td>&nbsp;</td> </tr> <tr> <tr> <td width=\"4%\" height=\"30\">&nbsp;</td> <td width=\"34%\" align=\"left\">&nbsp;</td> <td>&nbsp;</td> </tr> <tr> <tr> <td width=\"4%\" height=\"30\">&nbsp;</td> <td width=\"34%\" align=\"left\">&nbsp;</td> <td>&nbsp;</td> </tr> <tr> <td colspan=\"3\" align=\"center\"> <img width=\"525\" src=\"http://localhost:57263/Images/SignAreaForNatural.jpg\"> </td> </tr> <tr> <td width=\"4%\" height=\"30\">&nbsp;</td> <td width=\"34%\" align=\"left\">&nbsp;</td> <td>&nbsp;</td> </tr> </table> </body></html>";
            //    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(html);
            //    MemoryStream stream = new MemoryStream(bytes);
            //    StreamReader streamReader = new StreamReader(stream);
            //    Document document = new Document(PageSize.A4);
            //    HTMLWorker hTMLWorker = new HTMLWorker(document);
            //    PdfWriter.GetInstance(document, new FileStream("", FileMode.Create));
            //    FontFactory.Register(Environment.GetEnvironmentVariable("windir") + "\\Fonts\\simhei.ttf");
            //    FontFactory.Register(Environment.GetEnvironmentVariable("windir") + "\\Fonts\\MINGLIU.TTC");
            //    FontFactory.Register(Environment.GetEnvironmentVariable("windir") + "\\Fonts\\KAIU.TTF");
            //    //FontFactory.Register(Environment.GetEnvironmentVariable("windir") + "\\Fonts\\Eudc.tte");
            //    //FontFactory.Register(Environment.GetEnvironmentVariable("windir") + "\\Fonts\\Eudc.ttf");
            //    StyleSheet styleSheet = new StyleSheet();
            //    styleSheet.LoadTagStyle("body", "face", "SIMHEI");
            //    styleSheet.LoadTagStyle("body", "encoding", "Identity-H");
            //    styleSheet.LoadTagStyle("body", "leading", "10,1.2");
            //    //styleSheet.LoadTagStyle("span", "face", "EUDC");
            //    //styleSheet.LoadTagStyle("td", "leading", "10,1.2");

            //    document.Open();
            //    //List<IElement> list = HTMLWorker.ParseToList(streamReader, styleSheet);
            //    System.Collections.ArrayList arrayList = HTMLWorker.ParseToList(streamReader, styleSheet);
            //    List<IElement> list = arrayList.Cast<IElement>().ToList();
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        Console.WriteLine(string.Concat(new object[]
            //{
            //    "第",
            //    i + 1,
            //    "個物件為: ",
            //    list[i]
            //}));
            //        document.Add(list[i]);
            //    }
            //    document.Close();
            //    streamReader.Close();
            #endregion

            #region VARBinaryWriteFile
            //using (MemoryStream ms = new MemoryStream((byte[])"0xEFBBBF4E6F2C4E616D652C4641586E756D6265722C456D61696C2C52656D61726B732C43726561746F720D0A312CE5A79AE59C8BE88FAF2C303237333533333334362C6161614061612E636F6D2CE6B8ACE8A9A6312C53797374656D0D0A322CE894A1E4BDB3E685A72C303237333133313334312C6262624062622E636F6D2CE6B8ACE8A9A6322C53797374656D0D0A332CE590B3E5BF97E5AE8F2C303238383035383638392C6363634063632E636F6D2CE6B8ACE8A9A6332C53797374656D0D0A342CE78E8BE5BF97E7A98E2C303233353332323637382C6464644064642E636F6D2CE6B8ACE8A9A6342C53797374656D0D0A352CE696B9E99B85E5B5902C303236313834393938322C6565654065652E636F6D2CE6B8ACE8A9A6352C53797374656D0D0A362CE894A1E9878DE590882C303232343731363030382C6666664066662E636F6D2CE6B8ACE8A9A6362C53797374656D0D0A372CE69E97E58589E7B48B2C303237383539333234352C6767674067672E636F6D2CE6B8ACE8A9A6372C53797374656D0D0A382CE999B3E5B7A6E5A6B92C303237353133303732312C6868684068682E636F6D2CE6B8ACE8A9A6382C53797374656D0D0A392CE7A88BE78F8DE88B932C303237313736363136322C6969694069692E636F6D2CE6B8ACE8A9A6392C53797374656D0D0A31302CE5BCB5E5BBBAE4BD912C303237323731393932332C6A6A6A406A6A2E636F6D2CE6B8ACE8A9A631302C53797374656D0D0A"))
            //{
            //    using (FileStream file = new FileStream("D:\\BlackList.csv", FileMode.Create, System.IO.FileAccess.Write))
            //    {
            //        byte[] bytes = new byte[ms.Length];
            //        ms.Read(bytes, 0, (int)ms.Length);
            //        file.Write(bytes, 0, bytes.Length);
            //        ms.Close();
            //    }
            //}
            #endregion

            #region remove index -1 test
            //DataTable dtParticipants = new DataTable();

            //dtParticipants.Columns.Add("FormNo");
            //dtParticipants.Columns.Add("UserID");
            //dtParticipants.Columns.Add("DisplayName");

            //string custName = "";
            //DataTable dtCustName = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(dtParticipants));
            //foreach (DataRow dr in dtCustName.Rows)
            //{
            //    custName += (dr["CustName"].ToString() + ",");
            //}
            //if (!string.IsNullOrEmpty(custName))
            //    custName = custName.Remove(custName.Length - 1);
            #endregion

            #region ++i&i+=
            //int[] a = { 1, 2, 3, 4, 5 };
            //int b = 0;
            //for(int i = 0; i < a.Length; ++i)
            //{
            //    b += a[i];
            //    Console.WriteLine(i);
            //}
            //Console.WriteLine(b);
            #endregion

            //int _Out = 0;
            //if (!Int32.TryParse("0", out _Out))
            //{
            //    Console.WriteLine(_Out);
            //    if ("0" == "")
            //    {
            //        //_WS_CM_ADDRESS_NO_C01 = "0";
            //    }
            //    else
            //    {
            //        throw new System.Exception("WS_CM_ADDRESS_NO_C01  Must be Number.");
            //    }
            //}

            TEST3();
        }
        public static void TEST3()
        {
            try
            {
                TEST2();
            }
            catch (Exception ex)
            {
                Console.WriteLine("3");
            }
        }

        public static void TEST2()
        {
            try
            {
                TEST1();
            }
            catch (Exception ex)
            {
                Console.WriteLine("2");
            }
        }

        public static void TEST1()
        {
            try
            {
                throw new NullReferenceException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("1");
            }
        }


        public static void SetConnection(string SystemID, string ConnList)
        {
            string ConnSEC = string.Concat(new string[]
            {
                "Server=",
                "192.168.0.55",
                ";database=",
                "SEC",
                ";uid=",
                "apuser",
                ";pwd=",
                "1"
            });
            List<string> list = new List<string>();
            string[] array = ConnList.Split(new char[]
            {
                ','
            });
            for (int i = 0; i < array.Length; i++)
            {
                string text = array[i];
                if (!string.IsNullOrEmpty(text.Trim()))
                {
                    list.Add("ConnectionID='" + text + "'");
                }
            }
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnSEC))
                {
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = "select ConnectionID,ServerNameAES as ServerName,DatabaseNameAES as DatabaseName,UIDAES as UID,PasswordAES as Password,AdditionalConfig,Remarks,Requester,RequestDate,Creator,CreatedDate,Modifier,ModifiedDate from SecConnectionPool where " + string.Join(" or ", list.ToArray());

                        sqlConnection.Open();
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                string key = Convert.ToString(sqlDataReader["ConnectionID"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static byte[] ConvertRSAParametersField(Org.BouncyCastle.Math.BigInteger n, int size)
        {
            byte[] bs = n.ToByteArrayUnsigned();
            if (bs.Length == size)
                return bs;
            if (bs.Length > size)
                throw new ArgumentException("Specified size too small", "size");
            byte[] padded = new byte[size];
            Array.Copy(bs, 0, padded, size - bs.Length, bs.Length);
            return padded;
        }

        public static string HMACSHA256(string message, string key)
        {
            var encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(key);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacSHA256 = new HMACSHA256(keyByte))
            {
                byte[] hashMessage = hmacSHA256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashMessage);
            }
        }

        public static void ShowChars(char[] charArray)
        {
            Console.WriteLine("Char\tHex Value");
            // Display each invalid character to the console.
            foreach (char someChar in charArray)
            {
                if (Char.IsWhiteSpace(someChar))
                {
                    Console.WriteLine(",\t{0:X4}", (int)someChar);
                }
                else
                {
                    Console.WriteLine("{0:c},\t{1:X4}", someChar, (int)someChar);
                }
            }
        }

        public static string Decrypt(string toDecrypt, byte[] key, string iv)
        {
            var encryptBytes = System.Convert.FromBase64String(toDecrypt);
            var aes = new System.Security.Cryptography.RijndaelManaged();
            aes.Mode = System.Security.Cryptography.CipherMode.CBC;
            aes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            aes.Key = key;
            aes.IV = System.Text.Encoding.UTF8.GetBytes(iv);
            var transform = aes.CreateDecryptor();
            return System.Text.Encoding.UTF8.GetString(transform.TransformFinalBlock(encryptBytes, 0, encryptBytes.Length));
        }

        public static string SHA256_UTF16(string randomString)
        {

            Dictionary<string, object> firstLayerDic = new Dictionary<string, object>();
            firstLayerDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(randomString);

            InputParams inputParams = new InputParams();
            inputParams = JsonConvert.DeserializeObject<InputParams>(firstLayerDic["InputParams"].ToString());

            string encryptAssignCertPassword = string.Empty;

            if (inputParams.Action.Contains("SIGN"))
            {
                inputParams.PKCS7InputParams = null;//SIGN不需要PKCS7InputParams
            }
            else
            {
                inputParams.MIDInputParams = null;//PKCS7不需要MIDInputParams

                encryptAssignCertPassword = assignCertPasswordEncrypt("A123456789", inputParams.AssignCertPassword);

                inputParams.AssignCertPassword = encryptAssignCertPassword;
            }

            string dencryptAssignCertPassword = assignCertPasswordDecrypt("A123456789", "bwVnN9YBlu5UH7k88jgIbw==");

            string jsonInputParams = JsonConvert.SerializeObject(inputParams, Newtonsoft.Json.Formatting.None,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore//如果Value是null則跳過Json序列化
                        });

            var token = firstLayerDic["VerifyNo"].ToString().Replace("-", "");

            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.Unicode.GetBytes(firstLayerDic["BusinessNo"].ToString() + firstLayerDic["ApiVersion"].ToString() + firstLayerDic["HashKeyNo"].ToString() + firstLayerDic["VerifyNo"].ToString().Replace("-", "") + firstLayerDic["ReturnParams"].ToString() + jsonInputParams + "BBCCSSdev"));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash.ToLower();
        }

        public static string assignCertPasswordEncrypt(string key, string str)
        {
            //if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(str)) throw new Exception("Plaintext or Key is not valid");
            using (Aes myAes = Aes.Create())
            {
                // input converted to bytes
                byte[] inputBytes = Encoding.UTF8.GetBytes(str);

                // key converted to byte, and uppdate() + digest()
                var sha256 = SHA256Managed.Create();
                byte[] sha = Encoding.UTF8.GetBytes(key);
                byte[] tmp = sha256.ComputeHash(sha);

                //set initVector from keybytes
                byte[] keyBytes = new byte[16];
                byte[] initVector = new byte[16];
                //initVector = (byte[])tmp.Clone(); // Because it's 32 bytes, I get the former 16 bytes for IV
                for (int i = 0; i < 16; i++) keyBytes[i] = tmp[i];
                for (int i = 0; i < 16; i++) initVector[i] = tmp[i];

                //set the encrypted configure, and then encrpt the input
                myAes.Mode = CipherMode.CBC;
                myAes.Padding = PaddingMode.PKCS7;
                ICryptoTransform transform = myAes.CreateEncryptor(keyBytes, initVector);

                byte[] encryptedData = null;
                try
                {
                    encryptedData = transform.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
                    //Console.WriteLine(ToReadableByteArray(encryptedData));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    //throw new Exception("Fail to Encrypt the Data");
                }


                ////validate the key and initVector length( {128, 192, 256} and 128 separatedly )
                // https://stackoverflow.com/questions/1003275/how-to-convert-utf-8-byte-to-string
                //string key_s = Convert.ToBase64String(keyBytes);
                //string iv_s = Convert.ToBase64String(initVector);
                //examKeyLength(key_s, iv_s);

                return Convert.ToBase64String(encryptedData);
            }
        }

        public static string assignCertPasswordDecrypt(string key, string encryptedBase64Str)
        {
            using (Aes myAes = Aes.Create())
            {
                var sha256 = SHA256Managed.Create();
                byte[] sha = Encoding.UTF8.GetBytes(key);
                byte[] tmp = sha256.ComputeHash(sha);

                byte[] keyBytes = new byte[16];
                byte[] initVector = new byte[16];
                for (int i = 0; i < 16; i++) keyBytes[i] = tmp[i];
                for (int i = 0; i < 16; i++) initVector[i] = tmp[i];

                myAes.Mode = CipherMode.CBC;
                myAes.Padding = PaddingMode.PKCS7;
                ICryptoTransform transform = myAes.CreateDecryptor(keyBytes, initVector);

                byte[] decryptedData = null;
                try
                {
                    byte[] encryptedStr = Convert.FromBase64String(encryptedBase64Str);
                    decryptedData = transform.TransformFinalBlock(encryptedStr, 0, encryptedStr.Length);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    //throw new Exception("Fail to Decrypt the Encrpyted Data");
                }
                return Encoding.UTF8.GetString(decryptedData);
            }
        }

        public static void emailReplace()
        {
            string path = @"C:\Users\ASVT\Downloads\Email body1.html";
            File.OpenRead(path);
            if (!File.Exists(path))
                throw new ApplicationException("HTML檔：" + path + "，不存在喔~");

            StreamReader streamReader = new StreamReader(path, Encoding.GetEncoding("utf-8"));
            string content = streamReader.ReadToEnd();

            string[] mailItemList = { "[#acodateID]", "[#BranchName]", "[#BranchCode]", "[#acotypeID]", "[#acountID]", "[#acountID-FCY]", "[#DestTWD1]", "[#DestTWD2]" };

            string headContent = "";
            string footContent = "";
            string resultContent = content;

            if (true)
            {
                if (true)
                {
                    headContent = content.Substring(0, content.Substring(0, content.IndexOf("[#BranchCode]")).LastIndexOf("<B"));
                    footContent = content.Substring(content.IndexOf("[#BranchCode]"));
                    footContent = footContent.Substring(footContent.IndexOf("<B "));

                    resultContent = headContent + footContent;

                    headContent = resultContent.Substring(0, resultContent.Substring(0, resultContent.IndexOf("[#acountID]")).LastIndexOf("<B"));
                    footContent = resultContent.Substring(resultContent.IndexOf("[#acountID]"));
                    footContent = footContent.Substring(footContent.IndexOf("<B "));

                    resultContent = headContent + footContent;
                }

                if (true)
                {
                    headContent = resultContent.Substring(0, resultContent.Substring(0, resultContent.IndexOf("[#acountID-FCY]")).LastIndexOf("<B"));
                    footContent = resultContent.Substring(resultContent.IndexOf("[#acountID-FCY]"));
                    footContent = footContent.Substring(footContent.IndexOf("<B "));

                    resultContent = headContent + footContent;
                }

                if (true)
                {
                    headContent = resultContent.Substring(0, resultContent.Substring(0, resultContent.IndexOf("[#DestTWD1]")).LastIndexOf("<B"));
                    footContent = resultContent.Substring(resultContent.IndexOf("[#DestTWD1]"));
                    footContent = footContent.Substring(footContent.IndexOf("<B "));

                    resultContent = headContent + footContent;

                    headContent = resultContent.Substring(0, resultContent.Substring(0, resultContent.IndexOf("[#DestTWD2]")).LastIndexOf("<B"));
                    footContent = resultContent.Substring(resultContent.IndexOf("[#DestTWD2]"));
                    footContent = footContent.Substring(footContent.IndexOf("</B>"));

                    resultContent = headContent + footContent;
                }
            }

            //Console.WriteLine(content);
            Console.WriteLine(resultContent);
            streamReader.Close();
            streamReader.Dispose();
        }

        public static void base64test()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Console.WriteLine(System.Text.Encoding.GetEncoding("Big5").GetString(Convert.FromBase64String("QVAuSINPTjl=")).Replace("\0", ""));
            Console.WriteLine(System.Text.Encoding.GetEncoding("Big5").GetString(Convert.FromBase64String("AAAAAA==")));
        }

        public static void datetest()
        {
            string aa = "19601103";
            DateTime b = DateTime.ParseExact(aa, "yyyyMMdd", null);
            Console.WriteLine(b);
        }

        public static void GetEnPWD(string PWD)
        {
            int LeftShiftNum = 1;
            byte[] tmp = new byte[4];
            ASCIIEncoding Asc = new ASCIIEncoding();
            if (PWD.Length == 4)
            {
                tmp[0] = Convert.ToByte(Asc.GetBytes(PWD.Substring(0, 1))[0] + 50);
                tmp[1] = Convert.ToByte(Asc.GetBytes(PWD.Substring(1, 1))[0] + 52);
                tmp[2] = Convert.ToByte(Asc.GetBytes(PWD.Substring(2, 1))[0] + 54);
                tmp[3] = Convert.ToByte(Asc.GetBytes(PWD.Substring(3, 1))[0] + 56);
            }
            Asc = null;
            if (LeftShiftNum > 0)  //須左位移
            {
                for (int i = 0; i <= 3; i++)
                {
                    string strLShiftValue = "00000000" + Convert.ToString(Convert.ToInt32(tmp[i]) << LeftShiftNum, 2);
                    strLShiftValue = strLShiftValue.Substring(strLShiftValue.Length - 8, 8);
                    tmp[i] = Convert.ToByte(Convert.ToInt32(strLShiftValue, 2));
                }
            }
            string aa = Convert.ToBase64String(tmp);
            Console.WriteLine(aa);
            Console.WriteLine(System.Text.Encoding.GetEncoding(950).GetString(Convert.FromBase64String(aa)));
        }

        public static void JsonDeserial(string jsonstring)
        {
            TxIVR txIVR = new TxIVR();
            Dictionary<string, object> firstLayerDic = new Dictionary<string, object>();
            JObject J = JObject.Parse(jsonstring);
            jsonstring = J.SelectToken("AGROPENJSON").ToString();

            txIVR = JsonConvert.DeserializeObject<TxIVR>(jsonstring);

            //Return = JsonConvert.DeserializeObject<spsignatureRequestReturn>(jsonstring);
            string aa = "";

        }

        public static string Aes256Encode(byte[] tmp, string secretMessage)
        {
            using (Aes aes = new AesCryptoServiceProvider())
            {
                byte[] key = new byte[32];
                byte[] iv = new byte[16];

                for (int i = 0; i < 16; i++)
                {
                    iv[i] = key[i] = tmp[i];
                    key[i + 16] = tmp[i];
                }
                aes.KeySize = 256;
                aes.Key = key;
                aes.IV = iv;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;

                //Encrypt the message
                using (MemoryStream ciphertext = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ciphertext, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] plaintextMessage = Encoding.UTF8.GetBytes(secretMessage);
                        cs.Write(plaintextMessage, 0, plaintextMessage.Length);
                        cs.FlushFinalBlock();
                        cs.Close();

                        return Convert.ToBase64String(ciphertext.ToArray());
                    }
                }
            }
        }
        public static string Aes256Decode(byte[] tmp, string encryptedMessage)
        {
            using (Aes aes = new AesCryptoServiceProvider())
            {
                byte[] key = new byte[32];
                byte[] iv = new byte[16];

                for (int i = 0; i < 16; i++)
                {
                    iv[i] = key[i] = tmp[i];
                    key[i + 16] = tmp[i];
                }
                aes.KeySize = 256;
                aes.Key = key;
                aes.IV = iv;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;

                // Decrypt the message
                using (MemoryStream plaintext = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(plaintext, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        byte[] byteEncryptedMessage = Convert.FromBase64String(encryptedMessage);

                        cs.Write(byteEncryptedMessage, 0, byteEncryptedMessage.Length);
                        cs.FlushFinalBlock();
                        cs.Close();
                        string message = Encoding.UTF8.GetString(plaintext.ToArray());

                        return message;
                    }
                }
            }
        }

        public static string oldAes256Encode(byte[] key, string secretMessage)
        {
            using (Aes aes = new AesCryptoServiceProvider())
            {
                aes.KeySize = 256;
                aes.Key = key;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.ECB;

                //Encrypt the message
                using (MemoryStream ciphertext = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ciphertext, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] plaintextMessage = Encoding.UTF8.GetBytes(secretMessage);
                    cs.Write(plaintextMessage, 0, plaintextMessage.Length);
                    cs.FlushFinalBlock();
                    cs.Close();

                    return Convert.ToBase64String(ciphertext.ToArray());
                }
            }
        }
        public static string oldAes256Decode(byte[] key, string encryptedMessage)
        {
            using (Aes aes = new AesCryptoServiceProvider())
            {
                aes.KeySize = 256;
                aes.Key = key;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.ECB;

                // Decrypt the message
                using (MemoryStream plaintext = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(plaintext, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        byte[] byteEncryptedMessage = Convert.FromBase64String(encryptedMessage);

                        cs.Write(byteEncryptedMessage, 0, byteEncryptedMessage.Length);
                        cs.FlushFinalBlock();
                        cs.Close();
                        string message = Encoding.UTF8.GetString(plaintext.ToArray());

                        return message;
                    }
                }
            }
        }

        public static bool UnifiedNumberingCheck(string strUserID)
        {
            //參考 http://www.skrnet.com/skrjs/demo/js0161.htm
            //特定倍數
            int[] _nValue = { 1, 2, 1, 2, 1, 2, 4, 1 };
            char[] _cArray = strUserID.ToCharArray();
            int _nSum = 0;
            int _nTemp = 0;
            //將統編 各個位數 去乘 特定倍數 再將 十位數 跟 個位數 加總 
            for (int n = 0; n < 8; n++)
            {
                _nTemp = ((Convert.ToInt32(_cArray[n]) - 48) * _nValue[n]);
                _nSum += _nTemp / 10 + _nTemp % 10;
            }

            bool _bSuccess = false;
            //全部加總 整除於10 就是正確
            int _nCheck = _nSum % 5;
            if (_nCheck == 0)
            {
                _bSuccess = true;
            }
            //萬一 上述錯誤 有例外狀況 當第7碼為7時 加總再加1 能整除於10 也算正確
            else if (_cArray[6] == '7')
            {
                _nCheck = (_nSum + 1) % 5;
                if (_nCheck == 0)
                {
                    _bSuccess = true;
                }
            }
            return _bSuccess;
        }

        public static bool isNumber(string s)
        {
            Regex NumberPattern = new Regex("[^0-9.]");
            return !NumberPattern.IsMatch(s);
        }
    }



    public class TWCALoginReturn
    {
        string _BusinessNo = "";
        string _ApiVersion = "";
        string _HashKeyNo = "";
        string _VerifyNo = "";
        string _ResultCode = "";
        string _ReturnCode = "";
        string _ReturnCodeDesc = "";
        string _IdentifyNo = "";
        OutputParams _OutputParams = new OutputParams();

        [DataMember]
        public string BusinessNo
        {
            get { return _BusinessNo; }
            set { _BusinessNo = value; }
        }

        [DataMember]
        public string ApiVersion
        {
            get { return _ApiVersion; }
            set { _ApiVersion = value; }
        }

        [DataMember]
        public string HashKeyNo
        {
            get { return _HashKeyNo; }
            set { _HashKeyNo = value; }
        }

        [DataMember]
        public string VerifyNo
        {
            get { return _VerifyNo; }
            set { _VerifyNo = value; }
        }

        [DataMember]
        public string ResultCode
        {
            get { return _ResultCode; }
            set { _ResultCode = value; }
        }

        [DataMember]
        public string ReturnCode
        {
            get { return _ReturnCode; }
            set { _ReturnCode = value; }
        }

        [DataMember]
        public string ReturnCodeDesc
        {
            get { return _ReturnCodeDesc; }
            set { _ReturnCodeDesc = value; }
        }

        [DataMember]
        public string IdentifyNo
        {
            get { return _IdentifyNo; }
            set { _IdentifyNo = value; }
        }

        [DataMember]
        public OutputParams OutputParams
        {
            get { return _OutputParams; }
            set { _OutputParams = value; }
        }
    }
    public class InputParams
    {
        string _MemberNo = "";
        string _MemberNoType = "";
        string _Action = "";
        string _Plaintext = "";
        string _CAType = "";
        string _AssignCertPassword = "";
        string _ErrCodeType = "";
        string _IsCpntProcess = "";

        MIDInputParams _MIDInputParams = new MIDInputParams();
        PKCS7InputParams _PKCS7InputParams = new PKCS7InputParams();

        [DataMember]
        public string MemberNo
        {
            get { return _MemberNo; }
            set { _MemberNo = value; }
        }

        [DataMember]
        public string MemberNoType
        {
            get { return _MemberNoType; }
            set { _MemberNoType = value; }
        }

        [DataMember]
        public string Action
        {
            get { return _Action; }
            set { _Action = value; }
        }

        [DataMember]
        public string Plaintext
        {
            get { return _Plaintext; }
            set { _Plaintext = value; }
        }

        [DataMember]
        public string CAType
        {
            get { return _CAType; }
            set { _CAType = value; }
        }

        [DataMember]
        public string AssignCertPassword
        {
            get { return _AssignCertPassword; }
            set { _AssignCertPassword = value; }
        }

        [DataMember]
        public string ErrCodeType
        {
            get { return _ErrCodeType; }
            set { _ErrCodeType = value; }
        }

        [DataMember]
        public string IsCpntProcess
        {
            get { return _IsCpntProcess; }
            set { _IsCpntProcess = value; }
        }

        [DataMember]
        public MIDInputParams MIDInputParams
        {
            get { return _MIDInputParams; }
            set { _MIDInputParams = value; }
        }

        [DataMember]
        public PKCS7InputParams PKCS7InputParams
        {
            get { return _PKCS7InputParams; }
            set { _PKCS7InputParams = value; }
        }
    }
    public class MIDInputParams
    {
        string _Platform = "";
        MIDwInputParams _MIDwInputParams = new MIDwInputParams();

        [DataMember]
        public string Platform
        {
            get { return _Platform; }
            set { _Platform = value; }
        }

        [DataMember]
        public MIDwInputParams MIDwInputParams
        {
            get { return _MIDwInputParams; }
            set { _MIDwInputParams = value; }
        }
    }
    public class MIDwInputParams
    {
        string _Operator = "";
        string _Msisdn = "";
        string _Birthday = "";
        string _ClauseVer = "";
        string _HideMidBirthday = "";

        [DataMember]
        public string Operator
        {
            get { return _Operator; }
            set { _Operator = value; }
        }

        [DataMember]
        public string Msisdn
        {
            get { return _Msisdn; }
            set { _Msisdn = value; }
        }

        [DataMember]
        public string Birthday
        {
            get { return _Birthday; }
            set { _Birthday = value; }
        }

        [DataMember]
        public string ClauseVer
        {
            get { return _ClauseVer; }
            set { _ClauseVer = value; }
        }

        [DataMember]
        public string HideMidBirthday
        {
            get { return _HideMidBirthday; }
            set { _HideMidBirthday = value; }
        }
    }

    public class PKCS7InputParams
    {
        string _isURL = "";
        string _isCAVerify = "";
        string _isFCERT = "";
        List<string> _Contents = new List<string>();

        [DataMember]
        public string isURL
        {
            get { return _isURL; }
            set { _isURL = value; }
        }

        [DataMember]
        public string isCAVerify
        {
            get { return _isCAVerify; }
            set { _isCAVerify = value; }
        }

        [DataMember]
        public string isFCERT
        {
            get { return _isFCERT; }
            set { _isFCERT = value; }
        }

        [DataMember]
        public List<string> Contents
        {
            get { return _Contents; }
            set { _Contents = value; }
        }


    }
    public class OutputParams
    {
        string _MemberNo = "";
        string _Token = "";
        string _TimeStamp = "";
        MIDOutputParams _MIDOutputParams = new MIDOutputParams();

        [DataMember]
        public string MemberNo
        {
            get { return _MemberNo; }
            set { _MemberNo = value; }
        }

        [DataMember]
        public string Token
        {
            get { return _Token; }
            set { _Token = value; }
        }

        [DataMember]
        public MIDOutputParams MIDOutputParams
        {
            get { return _MIDOutputParams; }
            set { _MIDOutputParams = value; }
        }
    }
    public class MIDOutputParams
    {
        string _Platform = "";

        [DataMember]
        public string Platform
        {
            get { return _Platform; }
            set { _Platform = value; }
        }
    }

    public class TWCADOReturn
    {
        string _BusinessNo = "";
        string _ApiVersion = "";
        string _HashKeyNo = "";
        string _VerifyNo = "";
        string _Token = "";
        string _IdentifyNo = "";

        [DataMember]
        public string BusinessNo
        {
            get { return _BusinessNo; }
            set { _BusinessNo = value; }
        }

        [DataMember]
        public string ApiVersion
        {
            get { return _ApiVersion; }
            set { _ApiVersion = value; }
        }

        [DataMember]
        public string HashKeyNo
        {
            get { return _HashKeyNo; }
            set { _HashKeyNo = value; }
        }

        [DataMember]
        public string VerifyNo
        {
            get { return _VerifyNo; }
            set { _VerifyNo = value; }
        }

        [DataMember]
        public string Token
        {
            get { return _Token; }
            set { _Token = value; }
        }

        [DataMember]
        public string IdentifyNo
        {
            get { return _IdentifyNo; }
            set { _IdentifyNo = value; }
        }
    }

    public class TWCAQueryVerifyReturn
    {
        string _BusinessNo = "";
        string _ApiVersion = "";
        string _HashKeyNo = "";
        string _VerifyNo = "";
        string _ResultCode = "";
        string _ReturnCode = "";
        string _ReturnCodeDesc = "";
        string _IdentifyNo = "";

        TWCAQueryVerifyOutputParmas _OutputParams = new TWCAQueryVerifyOutputParmas();

        [DataMember]
        public string BusinessNo
        {
            get { return _BusinessNo; }
            set { _BusinessNo = value; }
        }

        [DataMember]
        public string ApiVersion
        {
            get { return _ApiVersion; }
            set { _ApiVersion = value; }
        }

        [DataMember]
        public string HashKeyNo
        {
            get { return _HashKeyNo; }
            set { _HashKeyNo = value; }
        }

        [DataMember]
        public string VerifyNo
        {
            get { return _VerifyNo; }
            set { _VerifyNo = value; }
        }

        [DataMember]
        public string ResultCode
        {
            get { return _ResultCode; }
            set { _ResultCode = value; }
        }

        [DataMember]
        public string ReturnCode
        {
            get { return _ReturnCode; }
            set { _ReturnCode = value; }
        }

        [DataMember]
        public string ReturnCodeDesc
        {
            get { return _ReturnCodeDesc; }
            set { _ReturnCodeDesc = value; }
        }

        [DataMember]
        public string IdentifyNo
        {
            get { return _IdentifyNo; }
            set { _IdentifyNo = value; }
        }

        [DataMember]
        public TWCAQueryVerifyOutputParmas OutputParmas
        {
            get { return _OutputParams; }
            set { _OutputParams = value; }
        }
    }


    public class TWCAQueryVerifyOutputParmas
    {
        string _Action = "";
        string _SelectType = "";
        string _VerifyTime = "";
        string _Plaintext = "";

        MIDParams _MIDParams = new MIDParams();
        CertParams _CertParams = new CertParams();
        PKCS7Params _PKCS7Params = new PKCS7Params();

        [DataMember]
        public string Action
        {
            get { return _Action; }
            set { _Action = value; }
        }

        [DataMember]
        public string SelectType
        {
            get { return _SelectType; }
            set { _SelectType = value; }
        }

        [DataMember]
        public string VerifyTime
        {
            get { return _VerifyTime; }
            set { _VerifyTime = value; }
        }

        [DataMember]
        public string Plaintext
        {
            get { return _Plaintext; }
            set { _Plaintext = value; }
        }

        [DataMember]
        public MIDParams MIDParams
        {
            get { return _MIDParams; }
            set { _MIDParams = value; }
        }

        [DataMember]
        public CertParams CertParams
        {
            get { return _CertParams; }
            set { _CertParams = value; }
        }

        [DataMember]
        public PKCS7Params PKCS7Params
        {
            get { return _PKCS7Params; }
            set { _PKCS7Params = value; }
        }
    }

    public class MIDParams
    {
        string _MIDResp = "";
        string _VerifyCode = "";
        string _VerifyMsg = "";

        [DataMember]
        public string MIDResp
        {
            get { return _MIDResp; }
            set { _MIDResp = value; }
        }

        [DataMember]
        public string VerifyCode
        {
            get { return _VerifyCode; }
            set { _VerifyCode = value; }
        }

        [DataMember]
        public string VerifyMsg
        {
            get { return _VerifyMsg; }
            set { _VerifyMsg = value; }
        }
    }
    public class CertParams
    {
        string _X509Cert = "";
        string _CertCN = "";
        string _CertSN = "";
        string _CertNotBefore = "";
        string _CertNotAfter = "";

        [DataMember]
        public string X509Cert
        {
            get { return _X509Cert; }
            set { _X509Cert = value; }
        }

        [DataMember]
        public string CertCN
        {
            get { return _CertCN; }
            set { _CertCN = value; }
        }

        [DataMember]
        public string CertSN
        {
            get { return _CertSN; }
            set { _CertSN = value; }
        }

        [DataMember]
        public string CertNotBefore
        {
            get { return _CertNotBefore; }
            set { _CertNotBefore = value; }
        }

        [DataMember]
        public string CertNotAfter
        {
            get { return _CertNotAfter; }
            set { _CertNotAfter = value; }
        }
    }
    public class PKCS7Params
    {
        string _PKCS7Signatures = "";
        string _PKCS7CAResults = "";

        [DataMember]
        public string PKCS7Signatures
        {
            get { return _PKCS7Signatures; }
            set { _PKCS7Signatures = value; }
        }

        [DataMember]
        public string PKCS7CAResults
        {
            get { return _PKCS7CAResults; }
            set { _PKCS7CAResults = value; }
        }
    }


    public class spsignatureRequestReturn
    {
        private string _tx_id = "";
        private string _salt = "";

        [DataMember]
        public string tx_id
        {
            get
            {
                return _tx_id;
            }

            set
            {
                _tx_id = value;
            }
        }

        [DataMember]
        public string salt
        {
            get
            {
                return _salt;
            }

            set
            {
                _salt = value;
            }
        }
    }
    public class TxTransferAccountOther_DataType
    {
        private string _strFLG_KIND = "";
        private string _strKIND = "";
        private string _strCurrency = "";
        private string _strAccountName = "";
        private string _strAccountName2 = "";
        private string _strAccountAdd = "";
        private string _strAccountBankName = "";
        private string _strAccount = "";
        private string _strSwiftCode = "";
        private string _strIDType = "";
        private string _strNationalType = "";
        public string FLG_KIND
        {
            get { return _strFLG_KIND; }
            set { _strFLG_KIND = value; }
        }

        public string KIND
        {
            get { return _strKIND; }
            set { _strKIND = value; }
        }

        public string Currency
        {
            get { return _strCurrency; }
            set { _strCurrency = value; }
        }
        public string AccountName
        {
            get { return _strAccountName; }
            set { _strAccountName = value; }
        }

        public string AccountName2
        {
            get { return _strAccountName2; }
            set { _strAccountName2 = value; }
        }

        public string AccountAdd
        {
            get { return _strAccountAdd; }
            set { _strAccountAdd = value; }
        }

        public string AccountBankName
        {
            get { return _strAccountBankName; }
            set { _strAccountBankName = value; }
        }

        public string Account
        {
            get { return _strAccount; }
            set { _strAccount = value; }
        }
        public string SwiftCode
        {
            get { return _strSwiftCode; }
            set { _strSwiftCode = value; }
        }
        public string IDType
        {
            get { return _strIDType; }
            set { _strIDType = value; }
        }
        public string NationalType
        {
            get { return _strNationalType; }
            set { _strNationalType = value; }
        }

    }

    [JsonObject(Title = "AGROPENJSON")]
    public class TxIVR
    {
        #region Init
        public void Init()
        {
            this._WS_IDNO = "";
            this._WS_EMPNO_7_TEL = "";
            this._WS_TELLER_5 = "";
            this._WS_EMPNO_7_MGR = "";
            this._WS_MGR_5 = "";
            this._WS_IVRA_CHANNEL = "";
            this._WS_IVRA_FUNCTION_CODE = "";
            this._WS_IVRA_COUNT_TOTAL = "";
            this._WS_IVRA_ADD_DEL_COUNT = "";
            this.WS_AGR_DATA = new List<IVRBiz_WS_AGR_DATA_Item>();
            this._WS_IVRA_REPLY_CODE = "";
            this._WS_IVRA_MSG = "";

        }
        #endregion

        #region Private Properties
        private string _WS_IDNO;
        private string _WS_EMPNO_7_TEL;
        private string _WS_TELLER_5;
        private string _WS_EMPNO_7_MGR;
        private string _WS_MGR_5;
        private string _WS_IVRA_CHANNEL;
        private string _WS_IVRA_FUNCTION_CODE;
        private string _WS_IVRA_COUNT_TOTAL;
        private string _WS_IVRA_ADD_DEL_COUNT;
        //private List<IBBiz_WS_AGR_DATA_Item> _WS_AGR_DATA;
        private string _WS_IVRA_REPLY_CODE;
        private string _WS_IVRA_MSG;
        #endregion

        #region Public Properties
        [JsonProperty(PropertyName = "WS-IDNO")]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property
        public string WS_IDNO { get { return _WS_IDNO; } set { _WS_IDNO = value; } }

        [JsonProperty(PropertyName = "WS-EMPNO-7-TEL")]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property
        public string WS_EMPNO_7_TEL { get { return _WS_EMPNO_7_TEL; } set { _WS_EMPNO_7_TEL = value; } }

        [JsonProperty(PropertyName = "WS-TELLER-5")]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property
        public string WS_TELLER_5 { get { return _WS_TELLER_5; } set { _WS_TELLER_5 = value; } }

        [JsonProperty(PropertyName = "WS-EMPNO-7-MGR")]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property
        public string WS_EMPNO_7_MGR { get { return _WS_EMPNO_7_MGR; } set { _WS_EMPNO_7_MGR = value; } }

        [JsonProperty(PropertyName = "WS-MGR-5")]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property
        public string WS_MGR_5 { get { return _WS_MGR_5; } set { _WS_MGR_5 = value; } }

        [JsonProperty(PropertyName = "WS-IVRA-CHANNEL")]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property
        public string WS_IVRA_CHANNEL { get { return _WS_IVRA_CHANNEL; } set { _WS_IVRA_CHANNEL = value; } }

        [JsonProperty(PropertyName = "WS-IVRA-FUNCTION-CODE")]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property
        public string WS_IVRA_FUNCTION_CODE { get { return _WS_IVRA_FUNCTION_CODE; } set { _WS_IVRA_FUNCTION_CODE = value; } }

        [JsonProperty(PropertyName = "WS-IVRA-COUNT-TOTAL")]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property
        public string WS_IVRA_COUNT_TOTAL { get { return _WS_IVRA_COUNT_TOTAL; } set { _WS_IVRA_COUNT_TOTAL = value; } }

        [JsonProperty(PropertyName = "WS-IVRA-ADD-DEL-COUNT")]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property
        public string WS_IVRA_ADD_DEL_COUNT { get { return _WS_IVRA_ADD_DEL_COUNT; } set { _WS_IVRA_ADD_DEL_COUNT = value; } }

        [JsonProperty(PropertyName = "WS-AGR-DATA")]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property
        public List<IVRBiz_WS_AGR_DATA_Item> WS_AGR_DATA { get; set; }

        [JsonProperty(PropertyName = "WS-IVRA-REPLY-CODE")]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property
        public string WS_IVRA_REPLY_CODE { get { return _WS_IVRA_REPLY_CODE; } set { _WS_IVRA_REPLY_CODE = value; } }

        [JsonProperty(PropertyName = "WS-IVRA-MSG")]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property
        public string WS_IVRA_MSG { get { return _WS_IVRA_MSG; } set { _WS_IVRA_MSG = value; } }
        #endregion
    }

    public class IVRBiz_WS_AGR_DATA_Item
    {
        public IVRBiz_WS_AGR_DATA_Item()
        {
            this.WS_IVRA_FLG_KIND = "";
            this.WS_IVRA_KIND = "";
            this.WS_IVRA_BANK = "";
            this.WS_IVRA_DEPACNO_OUT = "";
        }

        //要做為List物件中的{ get; set; }必填

        [JsonProperty(PropertyName = "WS-IVRA-FLG-KIND")]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property
        public string WS_IVRA_FLG_KIND { get; set; }

        [JsonProperty(PropertyName = "WS-IVRA-KIND")]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property
        public string WS_IVRA_KIND { get; set; }

        [JsonProperty(PropertyName = "WS-IVRA-BANK")]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property
        public string WS_IVRA_BANK { get; set; }

        [JsonProperty(PropertyName = "WS-IVRA-DEPACNO-OUT")]   //JSON Name Mapping 呼叫時會轉成這個欄位,回傳時會取這個欄位轉回這個Property
        public string WS_IVRA_DEPACNO_OUT { get; set; }
    }
}
