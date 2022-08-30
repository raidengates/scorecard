import 'dart:convert';

import 'package:shared_preferences/shared_preferences.dart';
import 'package:uuid/uuid.dart';
import '../model/game.dart';

class GlobalsVariable {
  static String gameKey = 'globals_variable_game';
  static String boardGameKey = 'globals_variable_boardGame';
  static Game game = new Game(Uuid().v4(), 0, "Game", 2, 0, [], []);
  static BoardGame boardGame = new BoardGame([]);

  static Future iniGlobals() async {
    final prefs = await SharedPreferences.getInstance();
    // Read value from shared preferences of game
    final gameString = prefs.getString(gameKey);
    if (gameString != null) {
      final gameJson = jsonDecode(gameString);
      game = Game.fromJson(gameJson);
    }

    // Read value from shared preferences of boardGame
    final boardGameString = prefs.getString(boardGameKey);
    if (boardGameString != null) {
      final boardGameJson = jsonDecode(boardGameString);
      boardGame = BoardGame.fromJson(boardGameJson);
    }
  }

  static void saveGame(Game game) async {
    final prefs = await SharedPreferences.getInstance();
    // Save value to shared preferences of game
    final gameString = jsonEncode(game.toJson());
    prefs.setString(gameKey, gameString);
  }

  static Future saveBoardGame(Game game) async {
    final prefs = await SharedPreferences.getInstance();
    final gameString = prefs.getString(boardGameKey);
    if (gameString != null) {
      final gameJson = jsonDecode(gameString);
      BoardGame boardGame = BoardGame.fromJson(gameJson);
      boardGame.games.add(game);
      final boardGameString = jsonEncode(boardGame.toJson());
      prefs.setString(boardGameKey, boardGameString);
    } else {
      BoardGame boardGame = new BoardGame([game]);
      final boardGameString = jsonEncode(boardGame.toJson());
      prefs.setString(boardGameKey, boardGameString);
    }
  }

  static void updateBoardGame(BoardGame boardGame) async {
    final prefs = await SharedPreferences.getInstance();
    var encoded = json.encode(boardGame);
    final boardGameString = jsonEncode(boardGame.toJson());
    // Save value to shared preferences of boardGame
    prefs.setString(boardGameKey, boardGameString);
  }

  static Game getGame() {
    return GlobalsVariable.game;
  }

  static Future<BoardGame> getBoardGame() async {
    final prefs = await SharedPreferences.getInstance();
    // Read value from shared preferences of boardGame
    final boardGameString = prefs.getString(boardGameKey);
    if (boardGameString != null) {
      final boardGameJson = jsonDecode(boardGameString);
      boardGame = BoardGame.fromJson(boardGameJson);
    }

    return GlobalsVariable.boardGame;
  }
}
