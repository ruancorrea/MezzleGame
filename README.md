<div align = "center">
   <h1>Projeto de Engenharia de Software</h1>
   <img src = "https://user-images.githubusercontent.com/47988061/70481044-211fda00-1ac0-11ea-902a-25dd01e7bc7a.png"></img>
</div>

[Arquivo executável disponível aqui para download via .zip](https://github.com/ruancorrea/MezzleGame/blob/master/Documentos/Mezzle%20Game.zip)


Mezzle é um jogo de plataforma 2D. O jogador terá que completar o quebra-cabeça com os elementos encontrados no jogo da memória em dado. O jogador vence o jogo caso tenha completado o quebra-cabeça, conseqüentemente, também completará o jogo da memória. No jogo, há a cronometragem do tempo e o acumulo de tentativas para acertar os pares do jogo da memória. Onde, o melhor, e menor, tempo de resolução de cada imagem em cada dificuldade, além do total de tentativas para completar o jogo da memória s]ao armazenados como tempo-recorde.

Como motor de jogo (engine) foi utilizado a Unity, para escrever os scripts (códigos) utilizamos o Visual Studio, que se integra com a Unity tendo como a linguagem C#,(pode ser C# ou C++) a linguagem utilizada. Usamos a ferramenta Collaborate da Unity para dar upload nas modificações efetuadas e assim conseguirmos compartilhar o projeto e sempre obter a versão atualizada para darmos prosseguimento ao projeto.
Podemos facilmente retornar a um ponto anterior utilizando o Collaborate, caso alguma modificação insira algum bug no projeto.
Foram utilizados para os diagramas as ferramentas Dia e LucidChart, e para redigir este relatório, o documento do google.

## Funcionamento do Jogo
Para iniciar um jogo, seleciona-se a opção “Start Game” no menu, logo em seguida terá a escolha da imagem que servirá como quebra-cabeça e jogo da memória. Após isso, vem a escolha da dificuldade do jogo, sendo:
 
•	Easy – montagem do quebra cabeça em 3x3 e 9 peças no jogo da memória (18 peças no total)

•	Medium – montagem do quebra-cabeça em 4x4 e 16 peças no jogo da memória (32 peças no total)

•	Hard – montagem do quebra-cabeça em 5x5 e 25 peças no jogo da memória (50 peças no total)


Utiliza-se o mouse para manusear as peças a serem escolhidas. Tendo que ao clicar botão direito podemos selecionar e move-lo. Caso erre o local da peça no quebra-cabeça, ela volta para o lugar de origem no quebra-cabeça. As teclas C e V controlam as telas do jogo. Sendo a tecla C designada para a tela de quebra-cabeça e a tecla V designada para a tela do jogo da memória. O objetivo é finalizar o quebra-cabeça no menor tempo possível. Com o jogo em andamento, clicando na tecla P, você pode pausar o jogo, parando o tempo a ser cronometrado, parar voltar ao jogo, basta clicar novamente na tecla P.

 
## Arquitetura em Camadas

<div align = "center">
 
<img src = "https://user-images.githubusercontent.com/47988061/73843886-45691000-47fe-11ea-8d6a-c1723ad96d0a.png"></img>

</div>

## Diagrama de casos de uso

<div align = "center">

![image](https://user-images.githubusercontent.com/47988061/73843959-66c9fc00-47fe-11ea-9773-f26cca4e7c44.png)

![image](https://user-images.githubusercontent.com/47988061/73843983-72b5be00-47fe-11ea-8123-701222f45500.png)

![image](https://user-images.githubusercontent.com/47988061/73844014-7f3a1680-47fe-11ea-9694-cd953e2478d1.png)

</div>



## Diagrama de Sequência

<div align = "center">

![Diagrama em branco (2)](https://user-images.githubusercontent.com/47988061/71024273-4e354380-20e3-11ea-8f53-c889db10204d.png)


![Diagrama em branco (1)](https://user-images.githubusercontent.com/47988061/73780506-5a499300-476d-11ea-9085-d6a4cec72e69.png)

</div>


## Diagrama de Classes

<div align = "center">

<img src = "https://user-images.githubusercontent.com/47988061/74073705-0a2b3480-49ea-11ea-9706-c801616e86d5.png"></img>

</div>

## Telas do Jogo
<div align = "center">
   <h3>Menu do Jogo</h3>
<img src = "https://user-images.githubusercontent.com/47988061/74073948-f0d6b800-49ea-11ea-99c8-bd32a797b923.png"></img>

   <h3>Instruções do Jogo</h3>
<img src = "https://user-images.githubusercontent.com/47988061/74073957-f7fdc600-49ea-11ea-968e-f15416c148b7.png"></img>

   <h3>Ao clicar em "Start Game", há o redirecionamento para a tela de escolha da imagem</h3>
<img src = "https://user-images.githubusercontent.com/47988061/74073953-f502d580-49ea-11ea-9635-c9797d0f439d.png"></img>

   <h3>Ao escolher a imagem, é a vez de escolher a dificuldade</h3>
<img src = "https://user-images.githubusercontent.com/47988061/74073963-00ee9780-49eb-11ea-846d-c1f6a6a6de97.png"></img>

   <h3>Jogo em andamento, tempo rolando na tela do jogo da memória. Dificuldade Easy escolhida como modelo</h3>
<img src = "https://user-images.githubusercontent.com/47988061/74073967-04821e80-49eb-11ea-85e7-10e548bb77a6.png"></img>
   
   <h3>Jogo pausado na tela do quebra-cabeça</h3>
<img src = "https://user-images.githubusercontent.com/47988061/74074442-d00f6200-49ec-11ea-998a-009bcea40704.png"></img>

   <h3>Jogo da memória e quebra-cabeça completados. Jogo finalizado. Mensagem de tempo recorde apareceu, logo novo recorde de tempo.</h3>
<img src = "https://user-images.githubusercontent.com/47988061/74073992-1499fe00-49eb-11ea-92bd-1b3897ecb420.png"></img>

   <h3>Créditos do Jogo</h3>
<img src = "https://user-images.githubusercontent.com/47988061/74073980-0cda5980-49eb-11ea-90f1-4c92b7767a07.png"></img>

</div>

## Desenvolvido por: 

  [João Pedro](https://github.com/joaopedrobritot)

  [Ruan Correa](https://github.com/ruancorrea)
