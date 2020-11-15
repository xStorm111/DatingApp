using API.Entities;

namespace API.Interfaces
{
    //Interfaces we allways prefix them with an I

    //Interfaces are a contract between itself and any class that implements it
    //What this means is that any class that implements the interface will implement the interface properties, method and/or events
    //Interface doesn't contain any implementation logic, only the signatures of the functionalities.
    public interface ITokenService
    {
        //In this case, the method CreateToken on TokenService will receive an AppUser and return a string.
        string CreateToken(AppUser user); //we can test token on jwt.io
    }
}