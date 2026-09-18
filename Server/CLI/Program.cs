using CLI.UI;
using FileRepositories;
using RepositoryContracts;

IUserRepository userRepository = new UserFileRepository();
ICommentRepository commentRepository = new CommentFileRepository();
IPostRepository postRepository = new PostFileRepositories();

CliApp cliApp = new CliApp(
    userRepository, postRepository, commentRepository);
    
    await cliApp.StartAsync();