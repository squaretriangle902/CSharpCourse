function evaluateExpression(expression) {
    let numberPattern = /(?:[0-9]+\.[0-9]+|[0-9]+)/g;
    let operatorPattern = /[+\-*\/]/g;
    let numbers = expression.match(numberPattern);
    let operators = expression.match(operatorPattern);
    let result = parseInt(numbers[0]);
    for (var i = 0; i < operators.length; i++) {
        result = evaluate(result, parseInt(numbers[i + 1]), operators[i]);
    }
    return result.toFixed(2);
}

function evaluate(leftOperand, rightOperand, operator) {
    switch (operator) {
        case "+":
            return leftOperand + rightOperand;
        case "-":
            return leftOperand - rightOperand;
        case "*":
            return leftOperand * rightOperand;
        case "/":
            return leftOperand / rightOperand;
        default:
            return 0;
    }
}

alert(evaluateExpression("1.003-0.5"));