import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class GameDetailPage extends StatefulWidget {
  @override
  State createState() {
    return _GameDetailPageState();
  }
}

class _GameDetailPageState extends State<GameDetailPage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Game Detail'),
      ),
      body: Center(
        child: Text('content game detail'),
      ),
    );
  }
}
