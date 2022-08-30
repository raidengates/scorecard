import 'package:flutter/material.dart';

import '../model/player_data.dart';

class PlayerItem extends StatelessWidget {
  int index;
  String name;
  PlayerData playerData;
  ValueChanged<String> onChanged;

  PlayerItem(
      {required this.index,
      required this.name,
      required this.playerData,
      required this.onChanged});
  @override
  Widget build(BuildContext context) {
    return Container(
      width: MediaQuery.of(context).size.width * 0.50 - 25,
      child: TextField(
        // controller: TextEditingController(
        //   text: name,
        // ),
        decoration: InputDecoration(
          labelText: 'Player name',
          labelStyle: TextStyle(fontSize: 20),
        ),
        onChanged: (e) => {
          playerData.name = e,
          onChanged(e),
        },
      ),
    );
  }
}
