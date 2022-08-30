import 'dart:convert';

import 'package:flutter/cupertino.dart';
import 'package:scorecard/model/game.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../utils/global.dart';

class BoardGameModel extends ChangeNotifier {
  BoardGame _currentBoardGame = new BoardGame([]);
  get currentBoardGame => _currentBoardGame;

  BoardGameModel() {
    getCurrent();
  }

  Future<BoardGame> getCurrent() async {
    final prefs = await SharedPreferences.getInstance();
    // Read value from shared preferences of boardGame
    final boardGameString = prefs.getString(GlobalsVariable.boardGameKey);
    if (boardGameString != null) {
      final boardGameJson = jsonDecode(boardGameString);
      _currentBoardGame = BoardGame.fromJson(boardGameJson);
    }
    return _currentBoardGame;
  }

  Future saveBoardGame(Game game) async {
    try {
      final prefs = await SharedPreferences.getInstance();
      final gameString = prefs.getString(GlobalsVariable.boardGameKey);
      if (gameString != null) {
        final gameJson = jsonDecode(gameString);
        BoardGame boardGame = BoardGame.fromJson(gameJson);
        boardGame.games.add(game);
        final boardGameString = jsonEncode(boardGame.toJson());
        prefs.setString(GlobalsVariable.boardGameKey, boardGameString);
        _currentBoardGame = boardGame;
      } else {
        BoardGame boardGame = new BoardGame([game]);
        final boardGameString = jsonEncode(boardGame.toJson());
        prefs.setString(GlobalsVariable.boardGameKey, boardGameString);
        _currentBoardGame = boardGame;
      }
    } finally {
      notifyListeners();
    }
  }
}
