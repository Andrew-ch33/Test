using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestApp.Models;
using TestApp.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TestApp.Ajax;

namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        public class LowercaseNamingStrategy : NamingStrategy
        {
            protected override string ResolvePropertyName(string name)
            {
                return name.ToLowerInvariant();
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Pal(string text)
        {
            if (string.IsNullOrEmpty(text))
                return AjaxResponse.Create(
                    new AjaxResult()
                    {
                        Code = (int)AjaxResultCode.Error,
                        ErrorMessage = "Введите строку для проверки",
                    }
                    ).ToJson();

            string rtext = text.Reverse();

            if (rtext.Equals(text, StringComparison.OrdinalIgnoreCase))
                return AjaxResponse.Create(
                    new AjaxResult()
                    {
                        Code = (int)AjaxResultCode.Ok,
                        Result = "Строка является палиндромом",
                    }
                    ).ToJson();
            else
                return AjaxResponse.Create(
                    new AjaxResult()
                    {
                        Code = (int)AjaxResultCode.Ok,
                        Result = "Строка не является палиндромом",
                    }
                    ).ToJson();
        }

        public IActionResult Sumarray(int[] arr)
        {
            if (arr == null || arr.Length == 0)
                return AjaxResponse.Create(
                    new AjaxResult()
                    {
                        Code = (int)AjaxResultCode.Error,
                        ErrorMessage = "Введите массив",
                    }
                    ).ToJson();

            int[] narr = arr.Where(x => x % 2 != 0).ToArray();

            if (narr.Length < 2)
                return AjaxResponse.Create(
                    new AjaxResult()
                    {
                        Code = (int)AjaxResultCode.Error,
                        ErrorMessage = "Недостаточно нечетных чисел",
                    }
                    ).ToJson();

            int sum = 0;

            for (int i = 0; i < narr.Length; i++)
            {
                if (i % 2 != 0)
                    sum += Math.Abs(narr[i]);
            }

            return AjaxResponse.Create(
                new AjaxResult()
                {
                    Code = (int)AjaxResultCode.Ok,
                    Result = sum,
                }
                ).ToJson();
        }

        private LinkedList<int> Sum2lists(LinkedList<int> a, LinkedList<int> b)
        {
            var mlength = Math.Max(a.Count, b.Count);
            LinkedList<int> res = new LinkedList<int>();
            int d = 0;
            LinkedListNode<int> anode = a.First;
            LinkedListNode<int> bnode = b.First;
            for (int i = 0; i < mlength+1; i++)
            {
                if (anode == null && bnode == null)
                {
                    if (d > 0)
                        res.AddLast(d);
                    break;
                }
                int sum = d + (anode != null ? anode.Value : 0) + (bnode != null ? bnode.Value : 0);
                d = (sum > 9) ? 1 : 0;
                res.AddLast((sum > 9) ? sum - 10 : sum);
                if (anode != null)
                    anode = anode.Next;
                if (bnode != null)
                    bnode = bnode.Next;
            }
            return res;
        }

        public IActionResult Sumlists(int a, int b)
        {
            string astr = a.ToString();
            var alist = new LinkedList<int>();
            foreach (char c in astr)
            {
                alist.AddFirst(int.Parse(c.ToString()));
            }

            string bstr = b.ToString();
            var blist = new LinkedList<int>();
            foreach (char c in bstr)
            {
                blist.AddFirst(int.Parse(c.ToString()));
            }

            var res = Sum2lists(alist, blist);

            return AjaxResponse.Create(
                new AjaxResult()
                {
                    Code = (int)AjaxResultCode.Ok,
                    Result = res,
                }
                ).ToJson();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
