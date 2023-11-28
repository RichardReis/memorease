import Colors from "../constants/Colors";
import Spacing from "../constants/Spacing";
import TextStyles from "./Text";
import { StyleSheet } from "react-native";

const HomeCardStyles = StyleSheet.create({
  danger: {
    width: "100%",
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",

    paddingHorizontal: Spacing.g,
    paddingVertical: Spacing.xg,

    borderRadius: Spacing.m,

    backgroundColor: Colors["light"].danger,
  },
  sucess: {
    width: "100%",
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",

    paddingHorizontal: Spacing.g,
    paddingVertical: Spacing.xg,

    borderRadius: Spacing.m,

    backgroundColor: Colors["light"].success,
  },
  neutral: {
    width: "100%",
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",

    paddingHorizontal: Spacing.g,
    paddingVertical: Spacing.xg,

    borderRadius: Spacing.m,

    backgroundColor: Colors["light"].neutral,
  },
  text: {
    ...TextStyles.title,
    color: Colors["light"].white,
    fontSize: 16,
  },
  textvalue: {
    ...TextStyles.title,
    color: Colors["light"].white,
    fontSize: 24,
  },
});

export const HomeCardColorStyles = StyleSheet.create({
  danger: {
    backgroundColor: Colors["light"].danger,
  },
  success: {
    backgroundColor: Colors["light"].success,
  },
  neutral: {
    backgroundColor: Colors["light"].neutral,
  },
});

export default HomeCardStyles;
