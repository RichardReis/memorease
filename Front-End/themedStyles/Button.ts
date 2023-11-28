import Colors from "../constants/Colors";
import Spacing from "../constants/Spacing";
import TextStyles from "./Text";
import { StyleSheet } from "react-native";

const ButtonStyles = StyleSheet.create({
  base: {
    height: 50,
    borderRadius: Spacing.m,
    paddingHorizontal: Spacing.m,
    flexDirection: "row",
    justifyContent: "center",
    alignItems: "center",
  },
  primary: {
    backgroundColor: Colors["light"].primary,
  },
  secondary: {
    backgroundColor: Colors["light"].contentBackground,
  },
  success: {
    backgroundColor: Colors["light"].success,
  },
  danger: {
    backgroundColor: Colors["light"].danger,
  },
  neutral: {
    backgroundColor: Colors["light"].neutral,
  },
  warning: {
    backgroundColor: Colors["light"].warning,
  },
});

export const ButtonTextStyles = StyleSheet.create({
  primary: {
    ...TextStyles.button,
  },
  secondary: {
    ...TextStyles.button,
    color: Colors["light"].text,
  },
  success: {
    ...TextStyles.button,
  },
  danger: {
    ...TextStyles.button,
  },
  neutral: {
    ...TextStyles.button,
  },
  warning: {
    ...TextStyles.button,
  },
});

export default ButtonStyles;
