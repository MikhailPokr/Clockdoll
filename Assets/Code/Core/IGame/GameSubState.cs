internal enum GameSubState
{
    PedroStartTurn = 12, //начинает ход
    PedroRollDice = 1, //бросает кубы
    PedroFortune= 2, //реагирует на фортуну, получает бафф
    PedroCardChoice = 3, //играет карту
    PedroCardPlay = 4, //Разыграл карту, сказал что-то
    AnokReaction = 5, //анок сбросил карты, если нужно
    AnokStartTurn = 6, //начинает ход
    AnokRollDice = 7, // ждем пока нажмет на лоток 
    AnokFortune= 8, //реагирует на фортуну, получает бафф
    AnokCardChoice = 9, //ждем пока чет сыграет
    AnokCardPlay = 10, //сыграл карту, бросил кости если надо
    PedroReaction = 11 //педро сбросил карты, если нужно
}
