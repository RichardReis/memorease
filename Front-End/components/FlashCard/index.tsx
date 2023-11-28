import React from "react";
import {
  View,
  StyleSheet,
  Text,
  Dimensions,
  TouchableOpacity,
} from "react-native";
import Colors from "../../constants/Colors";
import Spacing from "../../constants/Spacing";
import TextStyles from "../../themedStyles/Text";
import Icon from "../Icon";

const ITEM_WIDTH = Dimensions.get("window").width * 0.88;
const ITEM_HEIGHT = Dimensions.get("window").height * 0.88;

interface FlashCardProps {
  flip: boolean;
  front: string;
  back: string;
}

const FlashCard: React.FC<FlashCardProps> = ({ back, front, flip }) => {
  return (
    <View style={styles.flashcard}>
      <View style={styles.frontArea}>
        <TouchableOpacity
          style={{
            justifyContent: "flex-end",
            alignItems: "flex-end",
            width: "100%",
          }}
        >
          <Icon color="white" name="volume-high" />
        </TouchableOpacity>
        <Text style={styles.title}>Frente</Text>
        <Text style={styles.text}>{front}</Text>
      </View>
      {flip && (
        <>
          <View style={styles.backArea}>
            <TouchableOpacity
              style={{
                justifyContent: "flex-end",
                alignItems: "flex-end",
                width: "100%",
              }}
            >
              <Icon color="white" name="volume-high" />
            </TouchableOpacity>
            <Text style={styles.title}>Verso</Text>
            <Text style={styles.text}>{back}</Text>
          </View>
        </>
      )}
    </View>
  );
};

const styles = StyleSheet.create({
  flashcard: {
    backgroundColor: Colors["light"].primary,
    padding: Spacing.xg,
    marginVertical: Spacing.g,
    borderRadius: Spacing.g,

    width: ITEM_WIDTH,
    flex: 1,

    shadowColor: "#000",
    shadowOffset: {
      width: 0,
      height: 2,
    },
    shadowOpacity: 0.23,
    shadowRadius: 2.62,

    elevation: 4,

    borderWidth: 2,
    borderColor: Colors["light"].primary,
  },
  frontArea: {
    flex: 1,
    justifyContent: "center",
  },
  backArea: {
    height: "70%",
    justifyContent: "center",
    borderTopWidth: 1,
    borderTopColor: Colors["light"].white,
  },
  title: {
    ...TextStyles.subTitleHeader,
  },
  text: {
    ...TextStyles.titleHeader,
  },
});

export default FlashCard;
