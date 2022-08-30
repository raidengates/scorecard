// ignore_for_file: prefer_const_constructors

import 'dart:collection';

import 'package:convex_bottom_bar/convex_bottom_bar.dart';
import 'package:flutter/material.dart';

import '../model/badge.dart';
import '../model/choice_value.dart';
import '../model/named_color.dart';

class Data {
  static const String favorites = 'Favorites';
  static const String recents = 'Recents';
  static const String settings = 'Settings';

  static const gradients = [
    null,
    LinearGradient(
      begin: Alignment.topLeft,
      end: Alignment.bottomRight,
      colors: [Colors.blue, Colors.redAccent, Colors.green, Colors.blue],
      tileMode: TileMode.repeated,
    ),
    LinearGradient(
      begin: Alignment.center,
      end: Alignment(-1, 1),
      colors: [Colors.redAccent, Colors.green, Colors.blue],
      tileMode: TileMode.repeated,
    ),
    RadialGradient(
      center: const Alignment(0, 0), // near the top right
      radius: 5,
      colors: [Colors.green, Colors.blue, Colors.redAccent],
    )
  ];

  static const namedColors = [
    NamedColor(Colors.blue, 'Blue'),
    NamedColor(Color(0xFFf44336), 'Read'),
    NamedColor(Color(0xFF673AB7), 'Purple'),
    NamedColor(Color(0xFF009688), 'Green'),
    NamedColor(Color(0xFFFFC107), 'Yellow'),
    NamedColor(Color(0xFF607D8B), 'Grey'),
  ];

  static const badges = [
    null,
    Badge('1'),
    Badge('hot',
        badgeColor: Colors.orange, padding: EdgeInsets.only(left: 7, right: 7)),
    Badge('99+', borderRadius: 2)
  ];

  static const curves = [
    ChoiceValue<Curve>(
      title: 'Curves.bounceInOut',
      label: 'The curve bounceInOut is used',
      value: Curves.bounceInOut,
    ),
    ChoiceValue<Curve>(
      title: 'Curves.decelerate',
      value: Curves.decelerate,
      label: 'The curve decelerate is used',
    ),
    ChoiceValue<Curve>(
      title: 'Curves.easeInOut',
      value: Curves.easeInOut,
      label: 'The curve easeInOut is used',
    ),
    ChoiceValue<Curve>(
      title: 'Curves.fastOutSlowIn',
      value: Curves.fastOutSlowIn,
      label: 'The curve fastOutSlowIn is used',
    ),
  ];

  static const TitleColection = [
    'Favorites',
    'Recents',
    'Settings',
  ];

  static List<TabItem> items() {
    return [
      TabItem<IconData>(icon: Icons.favorite, title: favorites),
      TabItem<IconData>(icon: Icons.history, title: recents),
      TabItem<IconData>(icon: Icons.settings, title: settings),
    ];
  }
}
