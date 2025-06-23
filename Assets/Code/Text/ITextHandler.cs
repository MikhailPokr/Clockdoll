using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal interface ITextHandler : IService
{
    TextData ReturnJsonData(string jsonData, int page);
    TextData DataSearch(string searchedKey);
}
