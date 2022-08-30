import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class SettingsWidget extends StatelessWidget {
  final GestureTapCallback onPressed;
  final String text;
  const SettingsWidget({Key? key, required this.onPressed, required this.text})
      : super(key: key);

  @override
  Widget build(BuildContext context) => Scaffold(
        body: Center(
          child: Text('text'),
        ),
      );
}
