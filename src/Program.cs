using DbProjekt.Tables;
using Microsoft.VisualBasic;

namespace DbProjekt.src
{
    /// <summary>
    /// Main class only with Ui object 
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            UI ui = new UI();
            ui.Import();
        }
    }
 }

