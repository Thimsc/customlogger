namespace DevOnLogger.Models
{
    /// <summary>
    /// Table model which is associate with database table name "LogTable"
    /// </summary>
    public class LogTable
    {
        //Id: auto generated Id
        public int Id { get; set; }

        //Message : detailed information of error messages from client
        public string Message { get; set; }

        //OnDate: :log created on date
        public DateTime OnDate { get; set; }
    }

}
