using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquibbBot13K.Modules
{
    public class Feline : ModuleBase
    {
        public async Task HasBeenFed()
        {
            var PetName ="Tina";
            await ReplyAsync($"{PetName} has been fed. Don't fall for her bullshit!");
        }
    }
}
