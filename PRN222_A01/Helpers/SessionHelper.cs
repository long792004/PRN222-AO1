namespace NguyenThanhLong_SE18C.NET_A01.Helpers
{
    public static class SessionHelper
    {
        public const string USER_EMAIL = "UserEmail";
        public const string USER_ROLE = "UserRole";
        public const string USER_NAME = "UserName";
        public const string USER_ID = "UserId";

        public static class Roles
        {
            public const string ADMIN = "0";
            public const string STAFF = "1";
            public const string LECTURER = "2";
        }

        public static void SetUserSession(ISession session, string email, string role, string name, string userId)
        {
            session.SetString(USER_EMAIL, email);
            session.SetString(USER_ROLE, role);
            session.SetString(USER_NAME, name);
            session.SetString(USER_ID, userId);
        }

        public static bool IsAdmin(ISession session)
        {
            return session.GetString(USER_ROLE) == Roles.ADMIN;
        }

        public static bool IsStaff(ISession session)
        {
            return session.GetString(USER_ROLE) == Roles.STAFF;
        }

        public static bool IsLecturer(ISession session)
        {
            return session.GetString(USER_ROLE) == Roles.LECTURER;
        }

        public static bool IsAuthenticated(ISession session)
        {
            return !string.IsNullOrEmpty(session.GetString(USER_EMAIL));
        }

        public static short? GetCurrentUserId(ISession session)
        {
            var userId = session.GetString(USER_ID);
            if (short.TryParse(userId, out short id))
            {
                return id;
            }
            return null;
        }
    }
}
