import Colors from "../constants/Colors";
import { StyleSheet } from "react-native";
import Spacing from "../constants/Spacing";

const InputStyles = StyleSheet.create({
  regular: {
    height: 50,

    borderWidth: 1,
    borderRadius: Spacing.m,
    borderColor: Colors["light"].inputBorder,

    paddingHorizontal: Spacing.m,

    fontSize: 14,
    fontFamily: "Poppins",
    color: Colors["light"].titletext,
  },
});

export default InputStyles;
