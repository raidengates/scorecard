import 'package:convex_bottom_bar/convex_bottom_bar.dart';
import 'package:flutter/material.dart';
import 'package:scorecard/utils/data.dart';

import '../components/histories.widget.dart';
import '../notifier/board_game_model.dart';
import '../utils/global.dart';

class HomePage extends StatefulWidget {
  @override
  _HomePageState createState() => new _HomePageState();
}

class _HomePageState extends State<HomePage>
    with SingleTickerProviderStateMixin {
  final currentComponent = TextEditingController();

  TabController? _tabController;

  static int _selectedIndex = 0;
  var _isVisible;
  @override
  void initState() {
    super.initState();
    _tabController = TabController(length: Data.items().length, vsync: this);
    _isVisible = true;
  }

  @override
  Widget build(BuildContext context) {
    debugPrint('Console Message Using Debug Print');

    return Scaffold(
        appBar: AppBar(
          title: Text(Data.items()[_selectedIndex].title ?? ""),
        ),
        body: TabBarView(
          controller: _tabController,
          children: Data.items()
              .map((item) => item.title != Data.settings
                  ? HistoriesWidget(
                      onPressed: () {
                        debugPrint('select index');
                      },
                      text: item.title ?? "")
                  : Container())
              .toList(),
        ),
        floatingActionButton: new Visibility(
            visible: _isVisible,
            child: FloatingActionButton(
              onPressed: () {
                //GlobalsVariable.demoAction();
                Navigator.of(context).pushNamed('/create_game');
              },
              child: const Icon(Icons.add),
            )),
        bottomNavigationBar: ConvexAppBar(
          items: Data.items(),
          style: TabStyle.textIn,
          curve: Data.curves.first.value,
          backgroundColor: Data.namedColors.first.color,
          gradient: Data.gradients.first,
          controller: _tabController,
          onTap: (int i) => {
            setState(() {
              ;
              _selectedIndex = i;
              _isVisible = i != (Data.items().length - 1);
            })
          },
        ));
  }
}
