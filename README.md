# Monogame-School-Project-1
This is a school project where we describe other repository about a game 2D using monogame.

Original repository: [The-Horde](https://github.com/MohamedAG2002/The-Horde)

From: [MohamedAG2002](https://github.com/MohamedAG2002) 

Alunos:
* Leonel Oliveira - 22522
* José Abreu - 22532

# Sobre o jogo

Um jogo onde deves defender a tua fortaleza contra hordas de zombies que se aproximam. Tu, o jogador, estás equipado com as armas modernas mais avançadas desde o apocalipse - uma caçadeira e uma pistola. Utiliza o teu arsenal para eliminar esses zombies e impedi-los de romper a barricada a todo o custo.

# Controlos

Todo o jogo é controlado através do teclado, incluído o menu.

Para jogar é utilizado o `WASD` ou `Teclas das setas`. Para mudar de arma `Q` ou `E`, `Q` para a pistola e o `E` para a caçadeira. Para disparar `Espaço`.

No menu, as diferentes opções tem a sua designada tecla ao lado. `Enter` para jogar. `H` para o ecrã de ajuda. `C` para o ecrã de créditos. `ESC` para fechar o jogo, mesmo quando se está a jogar. `M` para voltar para o menu. 

Outros controlos. `R` recomeçar um novo jogo, unicamente no ecrã de fim de jogo. `P` para colocar o jogo em pausa e retomar de pausa.

# Correção de erros:

O projeto em si vinha com 3 erros, mas eram do mesmo tipo.

O erro estava localizado na pasta `Content\Font`.

O problema é que ao dar build o compilador não identificava as fontes.

Para resolver apenas baixei a Font neste caso `Bit5x3` e coloquei dentro da pasta onde fica as fontes dei build e problema resolvido.

# Estrutura/Organização

A forma como o código está estruturado, faz com que se torne simples e rápido o seu entendimento, ao ler as várias linhas de código.

Como também a organização das `pastas/ficheiros` de cada classe que representa cada elemento essencial do jogo, da a sensação de algo limpo.

<strong>Estrutura das `pastas`</strong>

![Screenshot_2](https://github.com/Vulpix98/Monogame-School-Project-1/assets/75589500/1b1eba02-8157-4d9e-9681-0706071ef994)

<strong>Exemplo de um `ficheiro de código`</strong>

![Screenshot_3](https://github.com/Vulpix98/Monogame-School-Project-1/assets/75589500/a61e14c9-600a-4c74-8f5c-678a194a77e4)

# Fatores do Jogo

A primeira coisa que reparei acerca do jogo foi o tamanho do ecrâ que era relativamente pequeno, mas tem o seu proposito.

O tamanho do ecrâ é definido por `2 variaveis, com valores inteiros`, ou seja, não é algo que se ajusta automaticamente consoante o nivel ou outra coisa.

```c#
 protected override void Initialize()
 {
     // Utility variables init
     ScreenWidth = 384;
     ScreenHeight = 512;
     Random = new Random();

     // Changing the game window size
     _graphics.PreferredBackBufferWidth = ScreenWidth;
     _graphics.PreferredBackBufferHeight = ScreenHeight;

     _graphics.ApplyChanges();

     base.Initialize();
 }
```

![Screenshot_2](https://github.com/Vulpix98/Monogame-School-Project-1/assets/75589500/1fcf1a35-299c-4f29-b91d-ce8be23857d9)

Como podemos ver o tamanho do ecrâ fica assim, e outro fator que podemos falar em relação a esta imagem é o `menu`.

Em que o mesmo é composto por `ENTER = Play` iniciar o jogo, `H = Help` tutorial de como jogar e suas teclas, `C = Credits` fala das pessoas que estiveram envolvidas no `projeto/jogo`, `ESC = Exit` para sair e `M = Menu` caso seja necessário regressaro ao menu. 

# Entidades

As `Entidades` são os objetos que vão interagir dentro do cenário, em que elas são:
<ul>
 <li><strong>Player</strong></li>
 <li><strong>Zombie</strong></li>
 <li><strong>Bullet</strong></li>
</ul>

<stong>`Player`</strong> é o personagem que vamos controlar para nos defender do ataque, o mesmo só se pode mover para esquerda e direita `A` e `D`, o mesmo possui 2 armas, shotgun e pistola que pode ser trocadas apartir das teclas `Q` e `E`

Nele é difinido o tempo de espera de disparo de cada arma:

```c#
 #region Consts
  private const int MAX_PISTOL_COOLDOWN = 30;
  private const int MAX_SHOTGUN_COOLDOWN = 80;
 #endregion
```

<br></br>

<strong>`Zombie`</strong> é o inimigo do jogo, em que o memso vai destruir a fortaleza, o `Zombie` possui 3 tipos um `Basic` normal, `Brute` tem mais vida, mais dano e é mais lento, e por fim `Denizen` tem menos vida, menos dano mas é o mais rápido:

```c#
 #region Conts
  // Basic zombie consts
  private const int BASIC_HEALTH = 40;
  private const int BASIC_DAMAGE = 12;
  private const float BASIC_SPEED = 75.0f;
  
  // Brute zombie consts
  private const int BRUTE_HEALTH = 80;
  private const int BRUTE_DAMAGE = 40;
  private const float BRUTE_SPEED = 40.0f;
  
  // Denizen zombie consts
  private const int DENIZEN_HEALTH = 20;
  private const int DENIZEN_DAMAGE = 8;
  private const float DENIZEN_SPEED = 125.0f;
 #endregion
```

Os `Zombies` nascem randomicamente no início do mapa, e possuem um tempo de nascimento, e dependo do tempo a dificuldade do jogo pode aumentar, surgindo o `Zombie Brute` ou `Zombie Denizen`:

A variável `Random` é defenida no ficheiro principal `Game1`:

```c#
 Random = new Random();
```

Este codigo define a posição randomica de cada inimigo:

```c#
  // Reseting the position of the spawner to a new random position
  Position = m_SpawnPoints[Game1.Random.Next(0, m_SpawnPoints.Count - 1)];
```
O código abaixo é o que vai gerir o tempo de nascimento e também se é `Zombie Brute` ou `Zombie Denizen`:

```c#
 public void Update()
 {
     m_Timer++;

     // This timer will define the difficulty of the game.
     // Once this timer is passed a certain threshold, the zombies will begin to spawn more frequently.
     m_DifficultyTimer++;

     // The max time will decrease by 10 every 1000 ticks
     if(m_DifficultyTimer % 1000 == 0)
         m_MaxTime -= 10;

     if(m_Timer >= m_MaxTime)
     {
         // Adding a zombie
         if(m_SpawnCounter % 10 == 0) SpawnEntity(ZombieType.Brute);
         else if(m_SpawnCounter % 5 == 0) SpawnEntity(ZombieType.Denizen);
         else SpawnEntity(ZombieType.Basic);
         
         // Reseting the position of the spawner to a new random position
         Position = m_SpawnPoints[Game1.Random.Next(0, m_SpawnPoints.Count - 1)];

         // Reseting the timer
         m_Timer = 0;

         // Everytime a zombie spawns this counter will go up.
         // This will help with selecting which zombie to spawn next turn.
         // For example, if every 5  there is a brute zombie, and every 3 there is a denizen zombie.
         m_SpawnCounter++;
     }
 }
```

<br></br>

<strong>`Bullet`</strong> é o objeto que o `Player` vai usar para conseguir se defender dos inimigos, através das armas que o mesmo possui, `pistola` e `shotgun`.

O código abaixo são as variáveis que define o dano e a distância de cada bala de cada arma:

```c#
  // Pistol consts
  private const int PISTOL_MAX_DIST = 125;
  private const int PISTOL_DAMAGE = 20;

  // Shotgun consts
  private const int SHOTGUN_MAX_DIST = 75;
  private const int SHOTGUN_DAMAGE = 20;
```

# Fim/Pontuação 

O jogo em si funciona com base em fazer mais pontos antes da fortaleza ser destruída, em que essa fortaleza tem vida máxima de 400 e quando chegar ao fim dessa vida o jogo termina.

Dependendo de cada inimigo eliminar dá mais pontos ou menos:

<ul>
 <li>Zombie Basic -> 10</li>
 <li>Zombie Brute -> 15</li>
 <li>Zombie Denizen -> 10</li>
</ul>

```c#
public class ScoreManager
{
    #region Fields
    public int Score;
    public int HighScore;
    #endregion

    #region Constructor
    public ScoreManager()
    {
        Score = 0;
        HighScore = 0;

        // Subscribing to events
        Zombie.ScoreIncreaseEvent += OnScoreIncrease;
    }
    #endregion

    #region Methods
    public void Update()
    {
        // Setting a new high score if the current score is higher
        HighScore = Score > HighScore ? Score : HighScore;
    }

    public void OnScoreIncrease(ZombieType zombieType)
    {
        switch(zombieType)
        {
            case ZombieType.Basic:
                Score += 5;
                break;
            case ZombieType.Brute:
                Score += 15;
                break;
            case ZombieType.Denizen:
                Score += 10;
                break;
        }
    }
    #endregion
}
```

# Conclusão

Por fim, concluímos este trabalho, o jogo em si é bastante simples em termos de jogabilidade, entender o código em si também foi bastante claro, pois o desenvolvedor criou algo bem estruturado em que tudo é dividido por regiôes tornando tudo mais fácil.
