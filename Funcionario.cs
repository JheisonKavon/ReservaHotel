namespace Hotel{
    public class Funcionario : Pessoa{

        public string Senha{get;protected set;}
        
        public Funcionario(int id,string cpf, string nome, string telefone, string senha){
            Id = id;
            Cpf = cpf;
            Nome = nome;
            Telefone = telefone;
            Senha = senha;
        }

        public static Funcionario ProcuraFuncionario(Hotel hotel, string usuario, string senha){
            Funcionario funcionario = hotel.funcionarios.Find(f => usuario == f.Nome && senha == f.Senha);
            return funcionario;
        }
    }
}