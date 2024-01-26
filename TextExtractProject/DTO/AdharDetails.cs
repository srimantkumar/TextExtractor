namespace TextExtractProject.DTO 
{
    public class AdharDetails
    {
        //public AdharDetails(string fullName, string dOB, string gender, string address, string adharNumber)
        //{
        //    FullName = fullName;
        //    DOB = dOB;
        //    Gender = gender;
        //    Address = address;
        //    AdharNumber = adharNumber;
        //}

        public string FullName {get; set;}
        public string DOB {get; set;}
        public string Gender {get; set;}
        public string Address {get; set;}
        public string AdharNumber {get; set;}

        public override string ToString()
        {
            return $"FullName: {FullName}, DOB: {DOB}, Gender: {Gender}, Address: {Address}, AdharNumber: {AdharNumber}";
        }
    }
}