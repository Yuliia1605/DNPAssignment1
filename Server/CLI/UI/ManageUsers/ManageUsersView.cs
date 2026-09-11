using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUsersView
{
   private readonly IUserRepository userRepository;
   private readonly CreateUserView createUserView;
   private readonly ListUsersView listUsersView;
   
   public ManageUsersView(IUserRepository userRepository)
   {
      this.userRepository = userRepository;
      
      createUserView = new CreateUserView(userRepository);
      listUsersView = new ListUsersView(userRepository);
   }
}