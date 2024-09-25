# SuperJU

O **SuperJU** é um sistema de controle de estoque e vendas que permite o cadastro de clientes, produtos, gestão de entradas de produtos, vendas e a geração de relatórios detalhados de vendas e estoque. O sistema foi desenvolvido com **ASP.NET WebForms** como frontend, que se comunica com uma **API REST** construída em **.NET 8** seguindo o padrão **MVC**.

## Funcionalidades

- **Gestão de Produtos**: Pesquisa, cadastro e alteração de produtos.
- **Gestão de Clientes**: Pesquisa, cadastro e alteração de clientes.
- **Gestão de Entrada de Produtos**: Registro de entradas de produtos, atualização de estoque e preços.
- **Gestão de Pedidos**: Realização e visualização de pedidos, com controle de estoque.
- **Relatórios**: Geração de relatórios detalhados de vendas e estoque.

## Tecnologias Utilizadas

- **C# (.NET Framework 8.0)**
- **ASP.NET WebForms 4.8**
- **API REST (padrão MVC)**
- **ADO.NET** para acesso aos dados
- **Injeção de Dependências** para gerenciamento de serviços e repositórios
- **SQL Server** (banco de dados)
- **Docker** para containerização do banco de dados
- **DBeaver** para interação com o banco de dados
- **HttpClient** para comunicação entre WebForms e API
- **JSON** para formatação de dados
- **xUnit e Moq** para testes unitários

## Estrutura de Banco de Dados

O projeto utiliza o SQL Server com as seguintes tabelas principais:

- **PRODUTOS**: Gerenciamento de produtos disponíveis para venda.
- **CLIENTES**: Armazena informações dos clientes.
- **ENTRADAS_PRODUTO** e **ENTRADAS_PRODUTO_ITEM**: Registram as entradas de mercadorias e seus itens.
- **FORMAS_PAGAMENTO**: Contém as formas de pagamento disponíveis (Dinheiro, Cartão, Pix).
- **PEDIDOS** e **PEDIDOS_ITEM**: Controlam os pedidos e os itens de cada pedido.

## Docker Compose e Scripts SQL Utilizados

O banco de dados é configurado utilizando **Docker** e scripts SQL. Para subir o banco de dados, utilize o **Prompt de Comando do Desenvolvedor** no Visual Studio, apontando para a raiz do projeto, e execute os comandos:

- **Iniciar o banco de dados**:
  ```bash  
  docker-compose up -d
  
- **Desligar o banco de dados**:
  ```bash
  docker-compose down

O script SQL cria o banco de dados superju, o login superju_user e as permissões necessárias, além de criar as tabelas essenciais como **PRODUTOS**, **CLIENTES**, **ENTRADAS_PRODUTO**, **PEDIDOS**, e insere os métodos de pagamento.

## Comunicação entre WebForms e API

A comunicação entre o **ASP.NET WebForms** e a **API REST** foi estabelecida utilizando **HttpClient**, e a URL base da API foi configurada no arquivo web.config.

## Testes Unitários

A camada de serviços da API foi testada utilizando **xUnit** e **Moq**. Esses testes garantem que as regras de negócio foram implementadas corretamente e funcionam como esperado, sem a necessidade de uma conexão real com o banco de dados ou outras dependências externas.

## Como Executar o Projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/pradojuliana91/Projeto-Final-SuperJu.git

2. Suba o ambiente Docker:
    ```bash
   docker-compose up -d

3. Compile e execute o projeto WebForms e a API no Visual Studio.
4. Acesse o sistema no navegador e utilize as funcionalidades de controle de estoque e vendas.
   
## Contribuições   
  
Contribuições são bem-vindas! Para sugerir melhorias ou relatar problemas, abra uma issue ou envie um pull request.

## Créditos e Agradecimentos

Este projeto foi desenvolvido como parte do curso de formação em **.NET** oferecido pela **ATOS**, em parceria com a **Unicesumar**. Agradeço a oportunidade de aprendizado e desenvolvimento prático proporcionada por ambas as instituições. Um agradecimento especial também aos colegas que contribuíram durante o processo de desenvolvimento.

## Contato

Para perguntas ou sugestões, entre em contato:

- **Juliana do Prado Fernandes**
- Email: [pradojulianaf@gmail.com]
- GitHub: [https://github.com/pradojuliana91](https://github.com/pradojuliana91)

## Roadmap

- [ ] Adicionar autenticação de usuários.
- [ ] Implementar funcionalidades de relatórios avançados.
- [ ] Melhorar a interface do usuário.
- [ ] Integrar com plataformas de pagamento.

## Status do Projeto

Atualmente, o projeto está em fase de desenvolvimento e melhorias estão sendo realizadas continuamente. Fique atento para atualizações e novas funcionalidades!

---

Obrigado por conferir o projeto **SuperJU**! Esperamos que ele seja útil para você!


