namespace Base.Exceptions
{
    public static class Extensions
    {
        /// <summary>
        /// Gets the user message.
        /// </summary>
        /// <param name="message">the ErrorMessage</param>
        /// <returns>string</returns>
        public static string GetUserMessage(this ErrorMessage message)
        {
            string val = null;
            switch (message)
            {
                case ErrorMessage.BAD_PARAMETERS:
                    val = "Bad Parameters";
                    break;
                case ErrorMessage.ILLEGAL_ACCESS:
                    val = "Illegal Acess";
                    break;
                case ErrorMessage.ILLEGAL_OBJECT_TYPE:
                    val = "Illegal Object Type";
                    break;
                case ErrorMessage.ILLEGAL_OPERATION:
                    val = "Illegal Operation";
                    break;
                case ErrorMessage.INTERNAL_BAD_ARGUMENT:
                    val = "Invalid Argument was passed to the method";
                    break;
                case ErrorMessage.INTERNAL_ERROR:
                    val = "Invalid Error has occured";
                    break;
                case ErrorMessage.INVALID_DATA_FORMAT:
                    val = "Unable to parse the given data value";
                    break;
                case ErrorMessage.INVALID_DATA_TYPE:
                    val = "Unable to process the give data type";
                    break;
                case ErrorMessage.INVALID_FIELD:
                    val = "Invalid field";
                    break;
                case ErrorMessage.INVALID_OPERAND:
                    val = "Invalid Operant was passed as parameter";
                    break;
                case ErrorMessage.INVALID_OPERATION:
                    val = "Invalid Operation";
                    break;
                case ErrorMessage.INVALID_PARAM:
                    val = "Invalid parameter was passed";
                    break;
                case ErrorMessage.INVALID_PATH_PARAM:
                    val = "Invalid path parameters";
                    break;
                case ErrorMessage.INVALID_RESOURCE_TYPE:
                    val = "Invalid Resource Type";
                    break;
                case ErrorMessage.MISSING_ID:
                    val = "ID is missing";
                    break;
                case ErrorMessage.MISSING_OPERAND:
                    val = "Missing operand that was passed as parameter";
                    break;
                case ErrorMessage.MULTIPLE_RECORD_FOUND:
                    val = "Multiple Records found";
                    break;
                case ErrorMessage.NO_RECORD_FOUND:
                    val = "No Record was found";
                    break;
                case ErrorMessage.POST_DELETED_REC:
                    val = "Can create a revision of deleted record";
                    break;
                case ErrorMessage.POST_INDATA:
                    val = "POST Operations Requires Data";
                    break;
            }
            return val;
        }
    }
    public enum ErrorMessage
    {
        /// <summary>
        ///  bad parameters.
        /// </summary>
        BAD_PARAMETERS,
        /// <summary>
        ///  illegal access.
        /// </summary>
        ILLEGAL_ACCESS,
        /// <summary>
        ///  illegal object type.
        /// </summary>
        ILLEGAL_OBJECT_TYPE,
        /// <summary>
        ///  illegal operation.
        /// </summary>
        ILLEGAL_OPERATION,
        /// <summary>
        ///  invalid argument.
        /// </summary>
        INTERNAL_BAD_ARGUMENT,
        /// <summary>
        ///  internal error.
        /// </summary>
        INTERNAL_ERROR,
        /// <summary>
        ///  invalid data format.
        /// </summary>
        INVALID_DATA_FORMAT,
        /// <summary>
        ///  invalid data type.
        /// </summary>
        INVALID_DATA_TYPE,
        /// <summary>
        ///  invalid field.
        /// </summary>
        INVALID_FIELD,
        /// <summary>
        ///  invalid operand.
        /// </summary>
        INVALID_OPERAND,
        /// <summary>
        ///  invalid operation.
        /// </summary>
        INVALID_OPERATION,
        /// <summary>
        ///  invalid parameter.
        /// </summary>
        INVALID_PARAM,
        /// <summary>
        ///  invalid path parameters.
        /// </summary>
        INVALID_PATH_PARAM,
        /// <summary>
        ///  invalid resource type.
        /// </summary>
        INVALID_RESOURCE_TYPE,
        /// <summary>
        ///  missing id.
        /// </summary>
        MISSING_ID,
        /// <summary>
        ///  missing operand.
        /// </summary>
        MISSING_OPERAND,
        /// <summary>
        ///  multiple records found.
        /// </summary>
        MULTIPLE_RECORD_FOUND,
        /// <summary>
        ///  no record found.
        /// </summary>
        NO_RECORD_FOUND,
        /// <summary>
        ///  can create revision of deleted record.
        /// </summary>
        POST_DELETED_REC,
        /// <summary>
        ///  no data for POST operation.
        /// </summary>
        POST_INDATA
    }
}