using System;

namespace Hotel{
    public class QuartoLuxo : Quarto{
        public bool Suite{get;private set;}

        public QuartoLuxo(int numero,bool disponivel,double valorDiaria,int maxPessoas,bool suite){
            Numero = numero;
            Disponivel = disponivel;
            ValorDiaria = valorDiaria;
            MaxPessoas = maxPessoas;
            Suite = suite;
        }
        public void setSuite(bool suite){
            Suite = suite;
        }
    }
}