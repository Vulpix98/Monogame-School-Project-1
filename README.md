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

Para jogar é utilizado o `A` e o `D` ou `Teclas das setas`. Para mudar de arma `Q` ou `E`, `Q` para a pistola e o `E` para a caçadeira. Para disparar `Espaço`.

Outros controlos. `R` recomeçar um novo jogo, unicamente no ecrã de fim de jogo. `P` para colocar o jogo em pausa e retomar de pausa.

# Correção de erros:

O projeto em si tinha 3 erros, mas eram do mesmo tipo.

O erro estava localizado na pasta `Content\Font`.

O problema é que ao dar build o compilador não identificava as fontes.

Para resolver apenas baixei a Font neste caso `Bit5x3` e coloquei dentro da pasta onde fica as fontes dei build e problema resolvido.

# Estrutura/Organização

A forma como o código está estruturado, faz com que se torne simples e rápido o seu entendimento, ao ler as várias linhas de código.

Como também a organização das `pastas` de cada classe que representa cada elemento essencial do jogo, da a sensação de algo limpo.

<strong>Estrutura das `pastas`</strong>

![Screenshot_2](https://github.com/Vulpix98/Monogame-School-Project-1/assets/75589500/1b1eba02-8157-4d9e-9681-0706071ef994)

<strong>Exemplo de um `ficheiro de código`</strong>

![Screenshot_3](https://github.com/Vulpix98/Monogame-School-Project-1/assets/75589500/a61e14c9-600a-4c74-8f5c-678a194a77e4)

# Fatores do Jogo

A primeira coisa que reparámos acerca do jogo foi o tamanho do ecrã que era relativamente pequeno, mas tem o seu propósito.

O tamanho do ecrã é definido por `2 variáveis, com valores inteiros`, ou seja, não é algo que se ajusta automaticamente consoante o nível ou outra coisa.

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

Como podemos ver o tamanho do ecrã fica assim, e outro fator que podemos falar em relação a esta imagem é o `menu`.

Em que o mesmo é composto por `ENTER = Play` iniciar o jogo, `H = Help` tutorial de como jogar e suas teclas, `C = Credits` dá crédito às pessoas que fizeram os sons e texturas, `ESC = Exit` para sair e `M = Menu` caso seja necessário regressar ao menu. 

# Entidades

As `Entities` são os objetos que vão interagir dentro do cenário, nos quais são:
<ul>
 <li><strong>Player</strong></li>
 <li><strong>Zombie</strong></li>
 <li><strong>Bullet</strong></li>
</ul>

<stong>`Player`</strong> é a personagem que vamos controlar para nos defender do ataque, o mesmo só se pode mover para esquerda e direita `A` e `D`, o mesmo possui 2 armas, caçadeira e pistola que pode ser trocadas apartir das teclas `Q` e `E`

Nele é definido o tempo de espera de disparo de cada arma:

```c#
 #region Consts
  private const int MAX_PISTOL_COOLDOWN = 30;
  private const int MAX_SHOTGUN_COOLDOWN = 80;
 #endregion
```

<br></br>

<strong>`Zombie`</strong> é o inimigo do jogo, em que o mesmo vai destruir a fortaleza.

Ele possui 3 tipos:
<ul>
    <li><strong>Basic</strong> - normal;</li>
    <li><strong>Brute</strong> - tem mais vida, mais dano e é mais lento;</li>
    <li><strong>Denizen</strong> - tem menos vida, menos dano mas é o mais rápido.</li>
</ul>

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

Os `Zombies` nascem aleatoriamente no início do mapa, e possuem um tempo de nascimento, e dependendo do tempo a dificuldade do jogo aumenta, surgindo o `Zombie Brute` ou `Zombie Denizen`:

A variável `Random` é definida no ficheiro principal `Game1`:

```c#
 Random = new Random();
```

Este código define a posição aleatória de cada inimigo:

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

<strong>`Bullet`</strong> é o objeto que o `Player` vai usar para conseguir se defender dos inimigos, através das armas que o mesmo possui, `pistola` e `caçadeira`. A pistola dispara unicamente 1 bala, já a caçadeira dispara 3.

O código abaixo são as variáveis que define o dano e a eficácia máxima de cada bala conforme a distância de cada arma:

```c#
  // Pistol consts
  private const int PISTOL_MAX_DIST = 125;
  private const int PISTOL_DAMAGE = 20;

  // Shotgun consts
  private const int SHOTGUN_MAX_DIST = 75;
  private const int SHOTGUN_DAMAGE = 20;
```

# Fim/Pontuação 

O jogo em si funciona com base em fazer mais pontos antes da fortaleza ser destruída, em que essa fortaleza tem vida máxima de 400 e quando chegar a 0 o jogo termina.

```c#
public int BarricadeHealth = 400;

public void OnBarricadeCollision(Zombie zombie)
{
    BarricadeHealth -= zombie.Damage;

    zombie.Velocity = new Vector2(0.0f, 0.0f);
}
```

Dependendo de cada inimigo ao eliminar dá mais pontos ou menos:

<ul>
 <li>Zombie Basic -> 5</li>
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

# Sugestões de melhorias

Adicionar um pequeno icon de informação para o utilizador saber qual arma está a utilizar no momento, uma vez que, já existe os dois icons das armas no projeto, `Content\Gui`.

![imagem](https://github.com/Vulpix98/Monogame-School-Project-1/assets/75588226/4c2619ad-ac0d-4437-b396-b478ac5a8f46)


# Conclusão

Por fim, concluímos este trabalho, o jogo em si é bastante simples em termos de jogabilidade, entender o código em si também foi bastante claro, pois o desenvolvedor criou algo bem estruturado em que tudo é dividido por regiôes tornando tudo mais fácil.
