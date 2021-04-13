using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormaLib.DBControl.Tables;

namespace NormaLib.SessionControl
{
    public static class SessionControl
    {
        private static User currentUser = null;

        public static User CurrentUser => currentUser;
        public static UserRole CurrentUserRole => currentUser == null ? null : currentUser.Role;

        public static void SignIn(User u)
        {
            currentUser = u;
        }

        public static void SignOut()
        {
            currentUser = null;
        }

        public static bool AllowAdd_Cable => IsAdmin || IsMetrolog;
        public static bool AllowEdit_Cable => AllowAdd_Cable;
        public static bool AllowRemove_Cable => AllowEdit_Cable;

        public static bool AllowAdd_User => IsAdmin || IsMetrolog;
        public static bool AllowEdit_User => AllowAdd_User;
        public static bool AllowRemove_User => AllowAdd_User;

        public static bool AllowAdd_BarabanType => IsAdmin || IsMetrolog;
        public static bool AllowEdit_BarabanType => AllowAdd_BarabanType;
        public static bool AllowRemove_BarabanType => AllowAdd_BarabanType;

        public static bool AllowRemove_CableTest => IsAdmin || IsMetrolog;

        public static bool IsAdmin => currentUser == null ? false : CurrentUserRole.UserRoleId == UserRole.DBAdmin;
        public static bool IsMetrolog => currentUser == null ? false : CurrentUserRole.UserRoleId == UserRole.Metrolog;
        public static bool IsOperator => currentUser == null ? false : CurrentUserRole.UserRoleId == UserRole.Operator;

        

        public static bool SignedIn => currentUser != null;
    }
}
