using System.Numerics;
using Nethereum.Contracts;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;


namespace EstudoBlockchain.Operations
{
    public class TransferEtherium
    {
        public async Task Send()
        {
            //The URL endpoint for the blockchain network.
            string url = "http://localhost:7545";

            //Prompts for the account address.
            Console.Write("Insira o endereço da chave privada de sua carteira de origem: ");
            string accountAddress = Console.ReadLine();

            var account = new Account(accountAddress);

            var web3 = new Web3(account, url);

            Console.Write("Insira o endereço da chave pública para a carteira d          e destino:  ");
            string toAddress = Console.ReadLine();

            Console.Write("Insira valor a ser transferido: ");
            string amountString = Console.ReadLine();

            if (!decimal.TryParse(amountString, out decimal amount)){
                throw new Exception("Valor a ser transferido inválido");
            }

            try{
                var transferService = web3.Eth.GetEtherTransferService();

                var transaction = await transferService
                    .TransferEtherAndWaitForReceiptAsync(toAddress, amount);

                Console.Write($"Transação realizada com sucesso, hash: {transaction.TransactionHash}");
            }catch(Exception e){
                Console.WriteLine($"Erro: {e.Message}");
            }
        } 
    }
}
