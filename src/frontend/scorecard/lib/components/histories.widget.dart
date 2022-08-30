import 'package:flutter/material.dart';
import 'package:scorecard/model/game.dart';

import '../notifier/board_game_model.dart';

class HistoriesWidget extends StatefulWidget {
  final GestureTapCallback onPressed;
  final String text;
  const HistoriesWidget({Key? key, required this.onPressed, required this.text})
      : super(key: key);
  @override
  HistoriesWidgetState createState() => HistoriesWidgetState();
}

class HistoriesWidgetState extends State<HistoriesWidget> {
  final _boardGameModel = BoardGameModel();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SingleChildScrollView(
        padding: EdgeInsets.only(bottom: 20),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Container(
              padding: EdgeInsets.only(top: 20),
              child: Text(
                widget.text,
                style: TextStyle(fontSize: 20),
              ),
            ),
            FutureBuilder(
              future: _boardGameModel.getCurrent(),
              builder: (context, AsyncSnapshot<BoardGame> snapshot) {
                if (snapshot.hasData) {
                  return ListView.builder(
                    shrinkWrap: true,
                    itemCount: snapshot.data?.games.length,
                    itemBuilder: (context, index) {
                      return Card(
                        child: ListTile(
                          title: Text(snapshot.data?.games[index].name ?? ""),
                          subtitle: Text(
                              "${index + 1}/${snapshot.data?.games.length}"),
                          onTap: () {
                            Navigator.of(context).pushNamed('/game_detail',
                                arguments: snapshot.data?.games[index]);
                          },
                        ),
                      );
                    },
                  );
                } else {
                  return Center(child: CircularProgressIndicator());
                }
              },
            ),
          ],
        ),
      ),
    );
  }
}
