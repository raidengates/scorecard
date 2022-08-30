// copy of _ChoiceValue from flutter gallery
class ChoiceValue<T> {
  const ChoiceValue(
      {required this.value, required this.title, required this.label});

  final T value;
  final String title;
  final String label; // For the Semantics widget that contains title

  @override
  String toString() => '$runtimeType("$title")';
}
