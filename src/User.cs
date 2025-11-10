namespace ITPHG.Models;


public class User
{
    private Guid _id = Guid.NewGuid();

    private string _email;

    private string _passwordHash;

    private string _name;


    public Guid Id
    {
        get
        {
            return _id;
        }
        set
        {
            _id = value;
        }
    }

    public string Email
    {
        get
        {
            return _email;
        }

        set
        {
            _email = value;
        }
    }

    public string PasswordHash
    {
        get
        {
            return _passwordHash;
        }

        set
        {
            _passwordHash = value;
        }
    }

    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }

}