namespace DrawIo;
 
public interface ICommand
{
    void Execute(Project app);
    void Undo(Project app);
}
