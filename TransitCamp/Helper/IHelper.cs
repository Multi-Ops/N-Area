using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public interface IHelper
    {
        string GeneratePassword(int length);
        string EncodePassword(string pass, string salt);
        string base64Encode(string sData);
        string base64Decode(string sData);
        string GenerateRendomAdNO(int length);
    }
}
