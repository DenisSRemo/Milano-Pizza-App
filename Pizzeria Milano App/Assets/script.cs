using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AWSSDK { };
public class script : MonoBehaviour





 {
"Statement": [{
    "Effect": "Allow",
    "Action": [
        "dynamodb:DeleteItem",
        "dynamodb:GetItem",
        "dynamodb:PutItem",
        "dynamodb:Scan",
        "dynamodb:UpdateItem"
    ],
    "Resource": "arn:aws:dynamodb:us-west-2:123456789012:table/MyTable"
}]
}
    // Start is called before the first frame update
   
