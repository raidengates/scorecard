import 'package:convex_bottom_bar/convex_bottom_bar.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:scorecard/model/choice_value.dart';
import 'package:scorecard/utils/global.dart';
import 'package:uuid/uuid.dart';
import 'package:intl/intl.dart';
import '../components/choose_tab_item.dart';
import '../components/heading.dart';
import '../components/player.item.dart';
import '../components/radio_item.dart';
import '../model/game.dart';
import '../model/player_data.dart';
import '../notifier/board_game_model.dart';
import '../utils/data.dart';

class CreateGamePage extends StatefulWidget {
  @override
  State createState() {
    return _CreateGamePageState();
  }
}

class _CreateGamePageState extends State<CreateGamePage> {
  var _isVisibleSave;
  final _boardGameModel = BoardGameModel();
  final _formKey = GlobalKey<FormState>();
  final _Players = [2, 3, 4, 5];
  List<PlayerData> players =
      List.generate(2, (index) => new PlayerData('', Uuid().v4(), index));

  static final kTabTypes = [
    ChoiceValue<List<int>>(
      title: 'None',
      label: 'Unlimited',
      value: [0, 1, 2],
    ),
    ChoiceValue<List<int>>(
      title: 'Score',
      label: 'Limit score',
      value: [0, 1, 2],
    ),
    ChoiceValue<List<int>>(
      title: 'Round',
      label: 'Limit round',
      value: [0, 1, 2],
    ),
  ];
  var _tabItems = kTabTypes.first;
  int _inputValue = 0;
  @override
  void initState() {
    super.initState();
    _isVisibleSave = false;
  }

  @override
  Widget build(BuildContext context) {
    final double height = MediaQuery.of(context).size.height;
    final GlobalKey<ScaffoldState> _scaffoldKey = GlobalKey<ScaffoldState>();
    final ThemeData theme = Theme.of(context);
    return Scaffold(
        key: _scaffoldKey,
        appBar: AppBar(
          title: Text('New Game'),
        ),
        body: Container(
          padding: const EdgeInsets.only(left: 20, right: 20),
          child: Form(
            key: _formKey,
            child:
                Column(crossAxisAlignment: CrossAxisAlignment.start, children: [
              SizedBox(height: height * 0.01),
              DropdownButtonFormField(
                decoration: InputDecoration(
                  labelText: 'Number of Players',
                  labelStyle: TextStyle(
                    color: theme.primaryColor,
                  ),
                  suffixStyle: TextStyle(
                    color: theme.primaryColor,
                  ),
                ),
                value: 2,
                items: _Players.map((numberOfPlayer) {
                  return DropdownMenuItem(
                    value: numberOfPlayer,
                    child: Text('$numberOfPlayer players'),
                  );
                }).toList(),
                onChanged: (value) {
                  var playerNumber = value as int;

                  if (playerNumber < players.length) {
                    setState(() {
                      players = players.take(playerNumber).toList();
                    });
                  } else if (playerNumber > players.length) {
                    setState(() {
                      players.addAll(List.generate(
                          (playerNumber - players.length),
                          (index) => new PlayerData('', Uuid().v4(),
                              index + (playerNumber - players.length))));
                    });
                  }

                  _checkForm();
                },
              ),
              SizedBox(height: height * 0.01),
              Wrap(
                crossAxisAlignment: WrapCrossAlignment.center,
                spacing: 5,
                runSpacing: 5,
                children: players
                    .map((player) => PlayerItem(
                        index: player.index + 1,
                        name: player.name,
                        playerData: player,
                        onChanged: (e) => {_checkForm()}))
                    .toList(),
              ),
              SizedBox(height: height * 0.01),
              const Heading('Game rule'),
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                children: kTabTypes.map<Widget>((ChoiceValue<List<int>> type) {
                  return Expanded(
                    child: RadioItem<List<int>>(
                        type, _tabItems, _onTabItemTypeChanged),
                  );
                }).toList(),
              ),
              SizedBox(height: height * 0.01),
              renderInputRule(),
            ]),
          ),
        ),
        floatingActionButton: new Visibility(
            visible: _isVisibleSave,
            child: FloatingActionButton(
              onPressed: _onSave,
              child: const Icon(Icons.add),
            )));
  }

  void _onSave() {
    var _game = GlobalsVariable.getGame();
    _game.id = Uuid().v4();
    //set game rule
    switch (_tabItems.title) {
      case 'None':
        _game.mode = 0;
        break;
      case 'Score':
        _game.mode = 1;
        break;
      case 'Round':
        _game.mode = 2;
        break;
      default:
    }

    //set player data
    _game.players = players.map((e) => Player(e.name)).toList();
    //set game name
    _game.name = DateFormat('dd-MM-yyyy').format(DateTime.now());
    //set number of player
    _game.numberOfPlayers = players.length;
    //set number of score
    _game.numberOfScores = _inputValue;
    // GlobalsVariable.saveBoardGame(_game);
    _boardGameModel.saveBoardGame(_game);
    Navigator.pop(context);
    Navigator.of(context).pushNamed('/game_detail');
  }

  Widget renderInputRule() {
    switch (_tabItems.title) {
      case 'Score':
        return TextField(
          decoration: InputDecoration(
            labelText: 'Enter your points',
            labelStyle: TextStyle(
              color: Theme.of(context).primaryColor,
            ),
            suffixStyle: TextStyle(
              color: Theme.of(context).primaryColor,
            ),
          ),
          keyboardType: TextInputType.number,
          inputFormatters: <TextInputFormatter>[
            FilteringTextInputFormatter.digitsOnly
          ],
          onChanged: (value) {
            setState(() {
              _inputValue = value.isNotEmpty ? int.parse(value) : 0;
            });
            _checkForm();
          },
        );
      case 'Round':
        return TextField(
          decoration: InputDecoration(
            labelText: 'Enter your round',
            labelStyle: TextStyle(
              color: Theme.of(context).primaryColor,
            ),
            suffixStyle: TextStyle(
              color: Theme.of(context).primaryColor,
            ),
          ),
          keyboardType: TextInputType.number,
          inputFormatters: <TextInputFormatter>[
            FilteringTextInputFormatter.digitsOnly
          ],
          onChanged: (value) {
            setState(() {
              _inputValue = value.isNotEmpty ? int.parse(value) : 0;
            });
            _checkForm();
          },
        );
      default:
        return SizedBox.shrink();
    }
  }

  void _checkForm() {
    var resultPlayers = false;
    //check player emptry
    if (players.every(
        (player) => (player.name.isNotEmpty && player.name.length > 0))) {
      resultPlayers = true;
    }
    var resultInput = false;
    if (_tabItems.title != 'None') {
      if (_inputValue != 0) {
        resultInput = true;
      }
    } else {
      resultInput = true;
    }
    setState(() {
      _isVisibleSave = resultPlayers && resultInput;
    });
  }

  void _onTabItemTypeChanged(ChoiceValue<List<int>>? value) {
    if (value == null) {
      return;
    }
    setState(() {
      _tabItems = value;
    });
    _checkForm();
  }
}
