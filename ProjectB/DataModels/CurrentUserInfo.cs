namespace Restaurant
{
    public class CurrentUserInfo
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Address> Address { get; set; }
    }
}