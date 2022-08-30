import 'package:flutter/material.dart';
import 'package:scorecard/pages/home.page.dart';

import '../pages/create_game.page.dart';
import '../pages/game_detail.page.dart';
import '../utils/global.dart';

class MyApp extends StatefulWidget {
  @override
  State createState() => _State();
}

class _State extends State<MyApp> {
  @override
  void initState() {
    super.initState();
    GlobalsVariable.iniGlobals();
  }

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      initialRoute: "/",
      routes: {
        '/': (context) => HomePage(),
        '/game_detail': (context) => GameDetailPage(),
        '/create_game': (context) => CreateGamePage(),
      },
      debugShowCheckedModeBanner: false,
    );
  }
}
