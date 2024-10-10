using System.Numerics;
using Nethereum.Contracts;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;


namespace EstudoBlockchain.Operations
{
    public class Vote
    {
        public async Task MakeVoteWithPuplicKey(){
            //The URL endpoint for the blockchain network.
            string url = "http://localhost:7545";

            //The contract address.
            string address = "0xF9c49E7C465609870ed6d639c82B56D6D2539095";

            //The ABI for the contract.
            string ABI = File.ReadAllText("../ABIs/vote.json");

            //Creates the connecto to the network and gets an instance of the contract.
            Web3 web3 = new Web3(url);
            Contract voteContract = web3.Eth.GetContract(ABI, address);

            //Reads the vote count for Candidate 1 and Candidate 2
            BigInteger candidate1Function = await voteContract.GetFunction("candidate1").CallAsync<BigInteger>();
            int candidate1 = (int)candidate1Function;

            BigInteger candidate2Function = await voteContract.GetFunction("candidate2").CallAsync<BigInteger>();
            int candidate2 = (int)candidate2Function;

            Console.WriteLine("Votos Candidato 1: {0}", candidate1);
            Console.WriteLine("Votos Candidato 2: {0}", candidate2);

            //Prompts for the account address.
            Console.Write("Insira o endereço da chave privada de sua carteira: ");
            string accountAddress = Console.ReadLine();

            //Prompts for the users vote.
            int vote = 0;
            Console.Write("Digite 1 para votar no candidato 1, digite 2 para votar no candidato 2: ");
            Int32.TryParse(Convert.ToChar(Console.Read()).ToString(), out vote);
            Console.WriteLine("Você informou {0}", vote);

            //Executes the vote on the contract.
            try{
                HexBigInteger gas = new HexBigInteger(new BigInteger(400000));
                HexBigInteger value = new HexBigInteger(new BigInteger(0));                 
                string castVoteFunction = await voteContract.GetFunction("castVote").SendTransactionAsync(accountAddress, gas, value, vote);
                
                Console.WriteLine($"Voto confirmado: {castVoteFunction.HasHexPrefix}");
            }catch(Exception e){
                Console.WriteLine("Erro: {0}", e.Message);
            }
        } 
    
        public async Task MakeVote()
        {
            //The URL endpoint for the blockchain network.
            string url = "http://localhost:7545";

            //The contract address.
            string address = "0xF9c49E7C465609870ed6d639c82B56D6D2539095";

            //The ABI for the contract.
            string ABI = File.ReadAllText("../ABIs/vote.json");

            //Creates the connecto to the network and gets an instance of the contract.
            Web3 web3 = new Web3(url);
            Contract voteContract = web3.Eth.GetContract(ABI, address);

            //Reads the vote count for Candidate 1 and Candidate 2
            BigInteger candidate1Function = await voteContract.GetFunction("candidate1").CallAsync<BigInteger>();
            int candidate1 = (int)candidate1Function;

            BigInteger candidate2Function = await voteContract.GetFunction("candidate2").CallAsync<BigInteger>();
            int candidate2 = (int)candidate2Function;

            Console.WriteLine("Votos Candidato 1: {0}", candidate1);
            Console.WriteLine("Votos Candidato 2: {0}", candidate2);

            //Prompts for the account address.
            Console.Write("Insira o endereço da chave privada de sua carteira: ");
            string accountAddress = Console.ReadLine();

            //Prompts for the users vote.
            int vote = 0;
            Console.Write("Digite 1 para votar no candidato 1, digite 2 para votar no candidato 2: ");
            Int32.TryParse(Convert.ToChar(Console.Read()).ToString(), out vote);
            Console.WriteLine("Você informou {0}", vote);

            Account? account = null;
            Contract? voteContractWithAccount = null;

            try{
                account = new Account(accountAddress);
                Web3 web3WithAccount = new Web3(account, url);
                voteContractWithAccount = web3WithAccount.Eth.GetContract(ABI, address);

                Console.Write($"Carteira {accountAddress} conectada com sucesso");
            }catch(Exception e){
                Console.WriteLine("Erro: {0}", e.Message);
            }

            //Executes the vote on the contract.
            try{
                HexBigInteger gas = new HexBigInteger(new BigInteger(400000));
                HexBigInteger value = new HexBigInteger(new BigInteger(0));                 
                string castVoteFunction = await voteContractWithAccount.GetFunction("castVote").SendTransactionAsync(account.Address, gas, value, vote);
                
                Console.WriteLine($"Voto confirmado: {castVoteFunction}");
            }catch(Exception e){
                Console.WriteLine("Erro: {0}", e.Message);
            }
        } 
    }
}