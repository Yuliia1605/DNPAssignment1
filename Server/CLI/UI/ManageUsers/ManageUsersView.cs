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

   public async Task ShowAsync()
   {
      while (true)
      {
         Console.WriteLine("User Management");
               Console.WriteLine("1. Create user");
               Console.WriteLine("2. List users");
               Console.WriteLine("0. Back");
               
               string? choice = Console.ReadLine();
         
               if (choice == "1")
               {
                  await createUserView.ShowAsync();
               }
               else if (choice == "2")
               {
                  listUsersView.Show();
               }
               else if (choice == "0")
               {
                  return;
               }
      }
      
   }
}