namespace Restaurant
{
    public class AdminInfo
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Address> Address { get; set; }
    }
}