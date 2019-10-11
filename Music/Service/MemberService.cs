using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music.Entity;

namespace Music.Service
{
    interface MemberService
    {
        String Login(MemberLogin memberLogin);

        Member Register(Member member);

        Member GetInformation();
        void logout();
        String GetUploadAvatarUrl();
        String GetTokenFromLocalStorage();
    }
}
