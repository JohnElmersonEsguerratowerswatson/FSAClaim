namespace FSA.API.ServerResponseMessage
{
    public static class ServerResponseMessage
    {
        public static string InternalError { get { return "There was a problem processing your request."; } }

        public static string BadRequest { get { return "There was a problem evaluating your request."; } }


    }
}
