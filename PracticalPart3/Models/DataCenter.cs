namespace PracticalPart3.Models
{
    /// <summary>
    /// Movel view class that hold all the columns in the excel file
    /// </summary>
    public class DataCenter
    {
        /// <summary>
        /// Getter and Setter for Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Getter and Setter for Fiscal Year
        /// </summary>
        public string FiscalYear { get; set; }

        /// <summary>
        /// Getter and Setter for Fiscal Period
        /// </summary>
        public string FiscalPeriod { get; set; }

        /// <summary>
        /// Getter and Setter for Month
        /// </summary>
        public string Month { get; set; }

        /// <summary>
        /// Getter and Setter for Information Date
        /// </summary>
        public string InformationDate { get; set; }

        /// <summary>
        /// Getter and Setter for Branch
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        /// Getter and Setter for Service
        /// </summary>
        public string Service { get; set; }

        /// <summary>
        /// Getter and Setter for SSC Client
        /// </summary>
        public string SscClient { get; set; }

        /// <summary>
        /// Getter and Setter for Metric Name
        /// </summary>
        public string MetricName { get; set; }

        /// <summary>
        /// Getter and Setter for Value
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Getter and Setter for Metric Type
        /// </summary>
        public string MetricType { get; set; }
    }
}
