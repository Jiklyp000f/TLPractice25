public class User
{
    private string UserName { get; set; }
    private string UserAdress { get; set; }
    public string Name
    {
        get
        {
            return UserName;
        }
        set
        {
            UserName = value;
        }
    }

    public string Adress
    {
        get
        {
            return UserAdress;
        }
        set
        {
            UserAdress = value;
        }
    }

    public User()
    {
        UserName = "NO NAME";
        UserAdress = "NO ADRESS";
    }
}