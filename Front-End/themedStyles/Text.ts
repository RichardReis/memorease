import Colors from "../constants/Colors";
import { StyleSheet } from "react-native";

const TextStyles = StyleSheet.create({
  title: {
    fontSize: 32,
    color: Colors["light"].titletext,
    fontFamily: "PoppinsBold",
  },
  subtitle: {
    fontSize: 24,
    color: Colors["light"].titletext,
    fontFamily: "PoppinsBold",
  },
  deckTitle: {
    fontSize: 24,
    color: Colors["light"].titletext,
    fontFamily: "PoppinsBold",
  },
  titleHeader: {
    fontSize: 24,
    color: Colors["light"].white,
    fontFamily: "PoppinsBold",
  },
  subTitleHeader: {
    fontSize: 16,
    color: Colors["light"].white,
    fontFamily: "Poppins",
  },
  regular: {
    fontSize: 14,
    color: Colors["light"].text,
    fontFamily: "Poppins",
  },
  clipboard: {
    fontSize: 16,
    color: Colors["light"].white,
    fontFamily: "Poppins",
  },
  label: {
    fontSize: 16,
    color: Colors["light"].text,
    fontFamily: "Poppins",
  },
  labelBold: {
    fontSize: 16,
    color: Colors["light"].text,
    fontFamily: "PoppinsBold",
  },
  button: {
    fontSize: 16,
    color: Colors["light"].white,
    fontFamily: "Poppins",
  },
  link: {
    color: Colors["light"].primary,
  },
});

export default TextStyles;
