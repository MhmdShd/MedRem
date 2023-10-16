import 'dart:math';

import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:uuid/uuid.dart';
import 'package:intl/intl.dart';

final formatter = DateFormat.yMd();

const uuid = Uuid();

enum Categories { syrup, tablet, capsule, drop, inhaler, injection }
enum Frequency { daily, weekly, monthly, yearly}

var categoryIcons = {
  Categories.syrup: Image.asset('assets/icons/syrup.png'),
  Categories.tablet: Image.asset('assets/icons/tablets.png'),
  Categories.capsule: Image.asset('assets/icons/capsule.png'),
  Categories.drop: Image.asset('assets/icons/drop.png'),
  Categories.inhaler: Image.asset('assets/icons/inhaler.png'),
  Categories.injection: Image.asset('assets/icons/injection.png'),
};

class Expense {
  Expense({
    required this.name,
    required this.description,
    required this.dosage,
    required this.frequency,
    required this.interval,
    required this.categ,

  }) : id = uuid.v4();

  final String id;
  final String name;
  final String description;
  final double dosage;
  final Frequency frequency;
  final int interval;
  final Categories categ;

}

class ExpenseBucket {
  const ExpenseBucket({
    required this.category,
    required this.expenses,
  });

  ExpenseBucket.forCategory(
    List<Expense> allExpenses,
    this.category,
  ) 
  : expenses = allExpenses
    .where((expense) => expense.categ == category).toList();
  final Categories category;
  final List<Expense> expenses;

}
