class Game {
  String id;
  int mode;
  String name;
  int numberOfPlayers;
  int numberOfScores;
  List<Player> players;
  List<PointOfRound> pointOfRound;
  Game(this.id, this.mode, this.name, this.numberOfPlayers, this.numberOfScores,
      this.players, this.pointOfRound);

  Game.fromJson(Map<String, dynamic> json)
      : id = json['id'],
        mode = json['mode'],
        name = json['name'],
        numberOfPlayers = json['numberOfPlayers'],
        numberOfScores = json['numberOfScores'],
        players =
            List<Player>.from(json['players'].map((x) => Player.fromJson(x))),
        pointOfRound = List<PointOfRound>.from(
            json['pointOfRound'].map((x) => PointOfRound.fromJson(x)));

  Map<String, dynamic> toJson() => {
        'id': id,
        'mode': mode,
        'name': name,
        'numberOfPlayers': numberOfPlayers,
        'numberOfScores': numberOfScores,
        'players': players.map((x) => x.toJson()).toList(),
        'pointOfRound': pointOfRound.map((x) => x.toJson()).toList(),
      };
}

class Player {
  String playerName;
  Player(this.playerName);
  Player.fromJson(Map<String, dynamic> json) : playerName = json['playerName'];
  Map<String, dynamic> toJson() => {
        'playerName': playerName,
      };
}

class PointOfRound {
  String roundName;
  List<PointOfPlayer> points;
  PointOfRound(this.roundName, this.points);
  PointOfRound.fromJson(Map<String, dynamic> json)
      : roundName = json['roundName'],
        points = List<PointOfPlayer>.from(
            json['points'].map((x) => PointOfPlayer.fromJson(x)));
  Map<String, dynamic> toJson() => {
        'roundName': roundName,
        'points': points.map((x) => x.toJson()).toList(),
      };
}

class PointOfPlayer {
  String playerName;
  int point;
  int pointOfBonus;
  int total;
  PointOfPlayer(this.playerName, this.point, this.pointOfBonus, this.total);
  PointOfPlayer.fromJson(Map<String, dynamic> json)
      : playerName = json['playerName'],
        point = json['point'],
        pointOfBonus = json['pointOfBonus'],
        total = json['total'];
  Map<String, dynamic> toJson() => {
        'playerName': playerName,
        'point': point,
        'pointOfBonus': pointOfBonus,
        'total': total,
      };
}

class BoardGame {
  List<Game> games;
  BoardGame(this.games);
  BoardGame.fromJson(Map<String, dynamic> json)
      : games = List<dynamic>.from(json['games'])
            .map((e) => Game.fromJson(e))
            .toList();

  Map<String, dynamic> toJson() => {
        'games': games.map((x) => x.toJson()).toList(),
      };
}
