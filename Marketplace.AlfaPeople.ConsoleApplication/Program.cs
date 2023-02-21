using Marketplace.AlfaPeople.ConsoleApplication.Controller;
using Marketplace.AlfaPeople.ConsoleApplication.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.AlfaPeople.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            CrmServiceClient serviceClient = Singleton.GetService();
            ContaController contaController = new ContaController(serviceClient);

            Console.WriteLine("Digite 1 para criar ou alterar uma conta");
            Console.WriteLine("Digite 2 para deletar uma conta");

            var answerWhatToDo = Console.ReadLine();

            if (answerWhatToDo.ToString() == "1")
            {
                MakeCreateAndUpdate(contaController);
            }
            else
            {
                if (answerWhatToDo.ToString() == "2")
                {
                    MakeDelete(contaController);
                }
                else
                {
                    Console.WriteLine("Opção inválida, reinicie o aplicativo");
                }

                Console.ReadKey();
            }
        }

        private static void MakeDelete(ContaController contaController)
        {
            Console.WriteLine("Digite o id da conta que queira deletar");
            var accountId = Console.ReadLine();
            contaController.Delete(new Guid(accountId));
            Console.WriteLine("Deletado com sucesso!");
        }

        private static void MakeCreateAndUpdate(ContaController contaController)
        {
            Console.WriteLine("Aguarde enquanto a nova conta é criada");
            Guid accountId = contaController.Create();
            Console.WriteLine("Conta criada com sucesso!");

            Console.WriteLine($"https://legacy.crm2.dynamics.com/main.aspx?appid=15e09654-05aa-ed11-9885-000d3a888d06&pagetype=entityrecord&etn=account&id={accountId}");

            Console.WriteLine("Você deseja criar um telefone para essa conta? (S/N)");
            var answerToUpdate = Console.ReadLine();

            if (answerToUpdate.ToString().ToUpper() == "S")
            {
                Console.WriteLine("Por favor preencha o telefone");
                var newTelephone = Console.ReadLine();
                bool contaAtualizada = contaController.Update(accountId, newTelephone);

                if (contaAtualizada)
                    Console.WriteLine("Telefone inserido com sucesso!");
                else
                    Console.WriteLine("Erro na criação, reinicie a aplicação");
            }

            Console.ReadKey();
        }
    }
}
